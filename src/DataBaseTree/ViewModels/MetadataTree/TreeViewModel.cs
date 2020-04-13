using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;

namespace DBManager.Application.ViewModels.MetadataTree
{
    public class TreeViewModel : ViewModelBase
    {
        private TreeViewItemViewModelBase _selectedItem;

        public ObservableCollection<TreeViewItemViewModelBase> RootItems { get; }

        public ICollectionView TreeView { get; }

        public TreeViewItemViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public TreeViewModel()
        {
            RootItems = new ObservableCollection<TreeViewItemViewModelBase>();

            TreeView = new ListCollectionView(RootItems);
        }
    }
}
