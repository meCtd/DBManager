using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using DBManager.Application.View;
using DBManager.Application.ViewModels.MetadataTree;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default;
using DBManager.Default.Printers;
using DBManager.Default.Tree;
using DBManager.SqlServer.Printer;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels.Windows
{
    public class MainWindowViewModel : BindableBase
    {

        public TreeViewModel Tree { get; }

        public TreeSearchViewModel TreeSearch { get; }



        #region Fields

        private IPrinterFactory _printerFactory;

        private string _definitionText;

        private bool _isSaveinInProcess;


        #endregion

        #region Properties

        public ObservableCollection<KeyValuePair<string, object>> ItemProperties { get; }

        public string DefinitionText
        {
            get { return _definitionText; }
            set
            {
                SetProperty(ref _definitionText, value);

            }
        }


        #endregion

        public MainWindowViewModel()
        {
            ItemProperties = new ObservableCollection<KeyValuePair<string, object>>();
        }

        #region Commands

        #region ConnectCommand

        private DelegateCommand _connectCommand;

        public DelegateCommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new DelegateCommand(
                           Connect));
            }
        }

        private void Connect()
        {
            ConnectionWindow window = new ConnectionWindow();
            ConnectionWindowViewModel data = (ConnectionWindowViewModel)window.DataContext;
            if (window.ShowDialog() == true)
            {
                switch (data.SelectedBaseType)
                {
                    case DialectType.MsSql:
                        TreeRootViewModel root =
                            new TreeRootViewModel(new MsSqlObjectLoader(data.ConnectionData.Connection));
                        root.TreeChanged += (sender, e) => _searchMatches = null;

                        Root = new TreeRootViewModel[] { root };
                        _printerFactory = new MsSqlPrinterFactory();
                        break;
                }
            }
        }

        #endregion

        #region RemoveConnectionCommand

        private DelegateCommand _removeConnectionCommand;

        public DelegateCommand RemoveConnectionCommand
        {
            get
            {
                return _removeConnectionCommand ?? (_removeConnectionCommand = new DelegateCommand(
                           () => Root = null, CanRemove));
            }
        }

        private bool CanRemove()
        {
            return Root != null && !Root.First().IsLoadingInProcess;
        }
        #endregion

        #region RefreshCommand

        private DelegateCommand<MetadataViewModelBase> _refreshCommand;

        public DelegateCommand<MetadataViewModelBase> RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new DelegateCommand<MetadataViewModelBase>(Refresh, CanRefresh)); }
        }

        private void Refresh(MetadataViewModelBase o)
        {
            o.RefreshTreeItem();
            if (o.Model.IsPropertyLoaded)
                ShowProperties(o);
            o.Model.Definition = string.Empty;
        }

        private bool CanRefresh(MetadataViewModelBase o)
        {
            if (o == null)
                return false;

            return !o.Root.IsLoadingInProcess && !o.IsBusy && o.Root.IsConnected;
        }

        #endregion

        #region RestoreConnection

        private DelegateCommand _resotreCommand;

        public DelegateCommand RestoreCommand
        {
            get
            {
                return _resotreCommand ??
                       (_resotreCommand = new DelegateCommand(Restore, CanRestore));
            }
        }

        private void Restore()
        {
            Root.First().RestoreConnection(true);
        }

        private bool CanRestore()
        {
            return Root != null && !Root.First().IsConnected;
        }

        #endregion

        #region LoadPropertiesCommand

        private DelegateCommand<MetadataViewModelBase> _loadPropertiesCommand;

        public DelegateCommand<MetadataViewModelBase> LoadPropertiesCommand
        {
            get { return _loadPropertiesCommand ?? (_loadPropertiesCommand = new DelegateCommand<MetadataViewModelBase>(LoadProperties, CanLoadProperties)); }
        }

        private async void LoadProperties(MetadataViewModelBase obj)
        {
            if (obj.Model.IsPropertyLoaded)
                obj.Root.RefreshProperties(obj);
            await obj.Root.LoadProperties(obj);
            ItemProperties.Clear();
            ItemProperties.AddRange(obj.Model.Properties);
        }

        private bool CanLoadProperties(MetadataViewModelBase obj)
        {

            if (obj is CategoryViewModel || obj == null)
                return false;

            return !obj.Root.IsLoadingInProcess && !obj.IsBusy && obj.Root.IsConnected;
        }

        #endregion

        #region ShowDefinitionCommand

        private DelegateCommand<MetadataViewModelBase> _showDefinitionCommandCommand;

        public DelegateCommand<MetadataViewModelBase> ShowDefinitionCommand
        {
            get { return _showDefinitionCommandCommand ?? (_showDefinitionCommandCommand = new DelegateCommand<MetadataViewModelBase>(ShowDefinition, CanShowDefinition)); }
        }

        private async void ShowDefinition(MetadataViewModelBase obj)
        {
            obj.IsBusy = true;
            obj.Root.IsLoadingInProcess = true;
            try
            {
                if (!string.IsNullOrWhiteSpace(obj.Model.Definition))
                {
                    DefinitionText = obj.Model.Definition;
                    return;
                }
                if (obj.Type == MetadataType.Table)
                {
                    if (!obj.Model.IsChildrenLoaded(null).GetValueOrDefault(false))
                        await obj.Root.LoadModel(obj, MetadataType.All);
                    foreach (var child in obj.Model.Children)
                    {
                        if (!child.IsPropertyLoaded)
                        {
                            obj.Root.DbObjectLoader.Connection.InitialCatalog = obj.Model.DataBaseName;
                            await obj.Root.DbObjectLoader.LoadPropertiesAsync(child);
                            if (obj.Root.IsDefaultDatabase)
                                obj.Root.DbObjectLoader.Connection.InitialCatalog = string.Empty;
                        }
                    }
                }

                //get printer

                DefinitionText = obj.Model.Definition = _printerFactory.GetPrinter(obj.Model).GetDefinition(obj.Model);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                obj.IsBusy = false;
                obj.Root.IsLoadingInProcess = false;
            }

        }

        private bool CanShowDefinition(MetadataViewModelBase obj)
        {
            if (obj == null)
                return false;
            if (obj.Type == MetadataType.Table &&
                (!obj.Model.IsChildrenLoaded(MetadataType.Column).GetValueOrDefault(false) && !obj.Root.IsConnected))
                return false;
            return !(obj is CategoryViewModel) && _printerFactory.IsSupported(obj.Model) && !obj.IsBusy && !obj.Root.IsLoadingInProcess;
        }

        #endregion

        #region ShowPropertiesCommand

       private void ShowProperties(MetadataViewModelBase obj)
        {
            ItemProperties.Clear();
            if (obj.Model.IsPropertyLoaded)
                ItemProperties.AddRange(obj.Model.Properties);
        }

        private bool CanShowProperties(MetadataViewModelBase obj)
        {
            if (obj == null)
                ItemProperties.Clear();
            return obj != null;
        }

        #endregion

        #region SaveCommand

        private DelegateCommand _saveCommand;

        public DelegateCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new DelegateCommand(SaveExecute, () => Root != null && !_isSaveinInProcess)); }

        }

        private void SaveExecute()
        {
            _isSaveinInProcess = true;
            try
            {
                SaveFileDialog save = new SaveFileDialog()
                {
                    Filter = "Tree Files (*.tree)|*.tree"
                };
                if (save.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(save.FileName, FileMode.OpenOrCreate))
                    {
                        fs.SetLength(0);
                        DataContractSerializer saver = new DataContractSerializer(typeof(SaveData));
                        saver.WriteObject(fs, new SaveData(Root.First().DbObjectLoader, Root.First().Model));
                    }
                    MessageBox.Show("Tree was saved!", "Saving", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                _isSaveinInProcess = false;
            }
        }

        #endregion

        #region OpenCommand

        private DelegateCommand _openCommand;

        public DelegateCommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new DelegateCommand(Open)); }
        }

        private void Open()
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                Filter = "Tree Files (*.tree)|*.tree"
            };

            if (open.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(open.FileName, FileMode.Open))
                {

                    try
                    {
                        DataContractSerializer ser = new DataContractSerializer(typeof(SaveData));
                        SaveData save = (SaveData)ser.ReadObject(fs);
                        Root = new[] { new TreeRootViewModel(save.Root, save.ObjectLoader) };
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            switch (Root.First().DbObjectLoader.Connection.Type)
            {
                case DialectType.MsSql:
                    _printerFactory = new MsSqlPrinterFactory();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        #endregion

        #endregion
    }
}