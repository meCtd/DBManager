using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Default.Tree;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public abstract class MetadataViewModelBase : TreeViewItemViewModelBase
    {
        private bool _isBusy;

        public ICommand _refreshCommand;

        public abstract string Name { get; }

        public abstract MetadataType Type { get; }

        public MetadataViewModelBase MetadataParent => Parent as MetadataViewModelBase;
        public DbObjectViewModel ObjectParent  => Parent as DbObjectViewModel;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ICommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new RelayCommand((s) => LoadChildrenInternal(true)));

        protected MetadataViewModelBase(MetadataViewModelBase parent, bool canHaveChildren) : base(parent, canHaveChildren)
        {
            ExpandChanged += (sender, args) =>
            {
                if (args.New)
                {
                    LoadChildrenInternal();
                }
            };
        }

        public abstract void RemoveChildren();
        public abstract void LoadChildren();

        protected void LoadChildrenInternal(bool reload = false)
        {
            try
            {
                IsBusy = true;
                if (reload)
                    RemoveChildren();

                LoadChildren();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
