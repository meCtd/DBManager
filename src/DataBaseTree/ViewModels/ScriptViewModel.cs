using System;
using System.IO;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;

using DBManager.Default;

using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using Ninject;


namespace DBManager.Application.ViewModels
{
    public class ScriptViewModel : ViewModelBase
    {
        private const string SavedFileFormat = "{0} {1}";
        private const string UnSavedFileFormat = "{0} {1}*";

        private readonly string _fileName;
        private readonly string _rootName;

        private ICommand _executeCommand;
        private ICommand _saveCommand;

        private string _filePath;
        private string _sql;
        private bool _hasChanges;


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

        public string Name => string.Format(_currentFormat, _fileName, _rootName);

        public DialectType Dialect { get; }

        public ICommand ExecuteCommand => _executeCommand ??
                                          (_executeCommand = new RelayCommand(Execute));

        public ICommand SaveCommand => _saveCommand ??
                                          (_saveCommand = new RelayCommand(s => SaveInternal()));

        public ScriptViewModel(string filename, string rootName, DialectType dialect)
        {
            _fileName = filename;
            _rootName = rootName;

            Dialect = dialect;

            Highlighting = HighlightingManager.Instance.GetDefinition(dialect.ToString());
        }
        
        private void Execute(object obj)
        {
            if (string.IsNullOrEmpty(Sql))
                return;
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
