using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Execution;

using Framework.Extensions;
using Framework.Utils;

using ICSharpCode.AvalonEdit.Highlighting;

using Microsoft.Win32;

using Ninject;

using ExecutionContext = DBManager.Default.Execution.ExecutionContext;

namespace DBManager.Application.ViewModels.ScriptExecution
{
    public class ScriptViewModel : ViewModelBase
    {
        private const string SavedFileFormat = "{0} {1}";
        private const string UnSavedFileFormat = "{0} {1}*";
        private const string SqlFilter = "SQL-files (*.sql)|*.sql";

        public static IEnumerable<string> EmptyContexts { get; } = new[] { "..." };

        private readonly string _fileName;
        private readonly MetadataViewModelBase _root;


        private readonly CancellationTokenSource _tokenSource;

        private ICommand _executeCommand;
        private ICommand _saveCommand;
        private ICommand _openCommand;
        private ICommand _cancelCommand;

        private string _filePath;
        private string _sql;
        private string _executionContext;
        private string _currentFormat = SavedFileFormat;

        private bool _hasChanges;
        private bool _isBusy;

        private object _data;

        private IEnumerable<string> _availableСontexts;

        private ScriptResultViewModel _result;

        public IHighlightingDefinition Highlighting { get; }

        public string Sql
        {
            get => _sql;
            set
            {
                if (SetProperty(ref _sql, value))
                    HasChanges = true;
            }
        }

        public string ExecutionContext
        {
            get => _executionContext;
            set => SetProperty(ref _executionContext, value);
        }

        public object Data
        {
            get => _data;
            set => SetProperty(ref _data, value, (old, n) => (old as IDisposable)?.Dispose());
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public IEnumerable<string> AvailableСontexts => _availableСontexts ?? (_availableСontexts = GetContexts());

        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                if (SetProperty(ref _hasChanges, value))
                {
                    _currentFormat = value
                        ? UnSavedFileFormat
                        : SavedFileFormat;

                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Name => string.Format(_currentFormat, _fileName, _root.Name);

        public bool WithContext => Context.Resolver.Get<IDialectComponent>(Dialect.ToString()).Executor.HasContext;

        public DialectType Dialect => _root.Dialect;

        public ICommand ExecuteCommand => _executeCommand ??
                                          (_executeCommand = new RelayCommand(Execute, s => !IsBusy));

        public ICommand SaveCommand => _saveCommand ??
                                          (_saveCommand = new RelayCommand(s => SaveInternal()));

        public ICommand OpenCommand => _openCommand ??
                                       (_openCommand = new RelayCommand(s => OpenFileCommand()));


        public ICommand CancelCommand => _cancelCommand ??
                                         (_cancelCommand = new RelayCommand(s => _tokenSource.Cancel(), s => IsBusy));

        public ScriptResultViewModel Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ScriptViewModel(string filename, MetadataViewModelBase root)
        {
            _fileName = filename;
            _root = root;
            _root.Refreshed += (s, args) =>
            {
                _availableСontexts = null;
                OnPropertyChanged(nameof(AvailableСontexts));
            };

            Highlighting = HighlightingManager.Instance.GetDefinition(Dialect.ToString());
        }

        private IEnumerable<string> GetContexts()
        {
            IEnumerable<string> items = null;

            if (!WithContext)
                return Enumerable.Empty<string>();

            using (PerformOperation(() =>
            {
                _root.LoadChildrenAsync().GetAwaiter().GetResult();
                items = _root.Children.Select(s => s.Name);
            }))
                return items;
        }

        private async void Execute(object obj)
        {
            if (string.IsNullOrEmpty(Sql))
                return;

            var executor = Context.Resolver.Get<IDialectComponent>(Dialect.ToString()).Executor;
            var connection = Context.Resolver.Get<IConnectionData>(_root.Name);

            try
            {
                IsBusy = true;

                var result = await executor.ExecuteAsync(Sql, new ExecutionContext(connection, ExecutionContext, CancellationToken.None));

                Result = new ScriptResultViewModel(await BuildData(result.Reader), BuildMessage(result.Info));

            }
            catch (Exception e)
            {
                Result = new ScriptResultViewModel(e);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private IDisposable PerformOperation(Action action)
        {
            return new DisposableToken(this, s =>
            {
                IsBusy = true;
                action?.Invoke();
            }, s =>
            {
                IsBusy = false;
                Context.Resolver.Get<IWindowManager>().RunOnUi(CommandManager.InvalidateRequerySuggested);
            });
        }

        private string BuildMessage(IScriptExecutionInfo info)
        {
            var builder = new StringBuilder();

            var rows = info.StatementAffectedRows.ToArray();
            rows.ForEach(s => builder.AppendLine($"({s} row(s) affected)\n"));

            if (rows.Length == 0)
                builder.Append("Operation completed successfully!\n");

            builder.Append($"\nCompletion time : {info.ExecutionTime}");

            return builder.ToString();
        }

        private async Task<IEnumerable<DataTable>> BuildData(IDisposableToken<DbDataReader> reader)
        {
            var result = new List<DataTable>();
            await Task.Run(() =>
            {
                using (reader)
                {
                    do
                    {
                        var table = reader.Instance.ToTable();

                        if (table != null)
                            result.Add(table);

                    } while (!reader.Instance.IsClosed && reader.Instance.NextResult());
                }
            });

            return result;
        }

        private void SaveInternal()
        {
            SetupFileName();
            using (var stream = File.CreateText(_filePath))
            {
                stream.Write(Sql);
            }

            HasChanges = false;
        }

        private void OpenFileCommand()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = SqlFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            var result = dialog.ShowDialog(Context.Resolver.Get<IWindowManager>().CurrentWindow);

            if (result != true)
                return;

            using (var reader = new StreamReader(File.Open(dialog.FileName, FileMode.Open, FileAccess.Read)))
            {
                Sql = reader.ReadToEnd();
                _filePath = dialog.FileName;
                HasChanges = false;
            }
        }

        private void SetupFileName()
        {
            if (!string.IsNullOrEmpty(_filePath))
                return;

            var dialog = new SaveFileDialog
            {
                FileName = _fileName,
                Filter = SqlFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };


            dialog.ShowDialog(Context.Resolver.Get<IWindowManager>().CurrentWindow);
            _filePath = dialog.FileName;
        }

        public bool Close()
        {
            return true;
        }

    }
}
