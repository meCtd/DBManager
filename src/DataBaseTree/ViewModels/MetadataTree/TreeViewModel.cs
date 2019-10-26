using System.Collections.Generic;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels.MetadataTree
{
    public class TreeViewModel : BindableBase
    {
        private TreeViewItemViewModelBase _selectedItem;

        public IEnumerable<TreeViewItemViewModelBase> RootItems { get; }

        public TreeViewItemViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
    }
}
