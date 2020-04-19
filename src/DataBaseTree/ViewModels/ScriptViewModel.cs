using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;

using ICSharpCode.AvalonEdit.Highlighting;

using Microsoft.Win32;

using Ninject;

using ExecutionContext = DBManager.Default.Execution.ExecutionContext;


namespace DBManager.Application.ViewModels
{
    public class ScriptViewModel : ViewModelBase
    {
        private const string SavedFileFormat = "{0} {1}";
        private const string UnSavedFileFormat = "{0} {1}*";

        private readonly string _fileName;
        private readonly MetadataViewModelBase _root;

        private ICommand _executeCommand;
        private ICommand _saveCommand;

        private string _filePath;
        private string _sql;
        private string _executionContext;
        private bool _hasChanges;
        private bool _isBusy;
        private object _data;
        private IEnumerable<string> _availableСontexts;

        private string _currentFormat = SavedFileFormat;

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

        public DialectType Dialect => _root.Dialect;

        public ICommand ExecuteCommand => _executeCommand ??
                                          (_executeCommand = new RelayCommand(Execute));

        public ICommand SaveCommand => _saveCommand ??
                                          (_saveCommand = new RelayCommand(s => SaveInternal()));

        public ScriptViewModel(string filename, MetadataViewModelBase root)
        {
            _fileName = filename;
            _root = root;

            Highlighting = HighlightingManager.Instance.GetDefinition(Dialect.ToString());
        }

        private IEnumerable<string> GetContexts()
        {
            _root.LoadChildrenAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            return _root.Children.Select(s => s.Name);
        }

        private async void Execute(object obj)
        {
            if (string.IsNullOrEmpty(Sql))
                return;

            var executor = Context.Resolver.Get<IDialectComponent>(Dialect.ToString()).Executor;
            var connection = Context.Resolver.Get<IConnectionData>(_root.Name);

            try
            {
                Data = await executor.ExecuteAsync(Sql, new ExecutionContext(connection, ExecutionContext, CancellationToken.None));

            }
            catch (Exception e)
            {
            }

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

        private void SetupFileName()
        {
            if (!string.IsNullOrEmpty(_filePath))
                return;

            var dialog = new SaveFileDialog
            {
                FileName = _fileName,
                Filter = "SQL-files (*.sql)|*.sql",
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
