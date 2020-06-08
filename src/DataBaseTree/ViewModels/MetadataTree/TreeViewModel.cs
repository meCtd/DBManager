using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Application.ViewModels.Windows;
using Ninject;

namespace DBManager.Application.ViewModels.MetadataTree
{
    public class TreeViewModel : ViewModelBase
    {
        private ICommand _setSelectedItemCommand;
        private ICommand _copyItem;
        private ICommand _showDataGenerationConfigWindow;

        private TreeViewItemViewModelBase _selectedItem;

        public ObservableCollection<TreeViewItemViewModelBase> RootItems { get; }
        private void ExecuteShowDataGenerationConfigWindow()
        {
            var viewModel = new DataGenerationConfigurationViewModel();
            
            Context.Resolver
                .Get<IWindowManager>()
                .ShowWindow(viewModel);
        }

        public ICollectionView TreeView { get; }

        public TreeViewItemViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value,
                    (old, @new) =>
                    {
                        if (!ReferenceEquals(@new, null))
                            @new.IsSelected = true;

                        if (!ReferenceEquals(old, null))
                            old.IsSelected = false;

                    });
            }
        }

        public ICommand ShowDataGenerationConfigWindow => _showDataGenerationConfigWindow ?? 
            (_showDataGenerationConfigWindow = new RelayCommand(
                s => ExecuteShowDataGenerationConfigWindow()));

        public ICommand SetSelectedItemCommand => _setSelectedItemCommand ??
                                                  (_setSelectedItemCommand = new RelayCommand<TreeViewItemViewModelBase>(
                                                      item => SelectedItem = item,
                                                      item => RootItems
                                                          .TreeGetTraversal(s => s.Children, s => s)
                                                          .Any(s => ReferenceEquals(s, item))));

        public ICommand CopyItem => _copyItem ?? (_copyItem =
            new RelayCommand<TreeViewItemViewModelBase>(
                s => Clipboard.SetText(s.ToString()), s => s is DbObjectViewModel));

        public TreeViewModel()
        {
            RootItems = new ObservableCollection<TreeViewItemViewModelBase>();

            TreeView = new ListCollectionView(RootItems);
        }
    }
}
