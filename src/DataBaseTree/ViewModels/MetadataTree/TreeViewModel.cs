using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;


namespace DBManager.Application.ViewModels.MetadataTree
{
    public class TreeViewModel : ViewModelBase
    {
        private ICommand _setSelectedItemCommand;

        private TreeViewItemViewModelBase _selectedItem;

        public ObservableCollection<TreeViewItemViewModelBase> RootItems { get; }

        public ICollectionView TreeView { get; }

        public TreeViewItemViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value,
                    (old, @new) =>
                    {
                        if (!ReferenceEquals(old, null))
                            old.IsSelected = false;

                        if (!ReferenceEquals(@new, null))
                            @new.IsSelected = true;

                    });
            }
        }

        public ICommand SetSelectedItemCommand => _setSelectedItemCommand ??
                                                  (_setSelectedItemCommand = new RelayCommand<TreeViewItemViewModelBase>(
                                                      item => SelectedItem = item,
                                                      item => RootItems
                                                          .TreeGetTraversal(s => s.Children, s => s)
                                                          .Any(s => ReferenceEquals(s, item))));

        public TreeViewModel()
        {
            RootItems = new ObservableCollection<TreeViewItemViewModelBase>();

            TreeView = new ListCollectionView(RootItems);
        }
    }
}
