using System.Threading.Tasks;
using System.Windows.Input;

using DBManager.Application.Loader;
using DBManager.Application.Utils;

using DBManager.Default;
using DBManager.Default.Tree;

using Ninject;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public abstract class MetadataViewModelBase : TreeViewItemViewModelBase
    {
        private const string BusyFormat = "{0} (loading...)";

        private bool _isBusy;

        private bool _wasLoaded;

        private ICommand _refreshCommand;

        public string Name => IsBusy
            ? string.Format(BusyFormat, ObjectName)
            : ObjectName;

        public abstract string ObjectName { get; }

        public abstract MetadataType Type { get; }

        public new MetadataViewModelBase Parent => base.Parent as MetadataViewModelBase;

        public virtual DialectType Dialect => Parent?.Dialect ?? DialectType.Unknown;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetProperty(ref _isBusy, value))
                    OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));

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

        protected abstract void RemoveChildrenFromModel();
        protected abstract Task LoadChildren();

        protected IObjectLoader GetLoader()
        {
            var current = this;
            while (current.Parent != null)
            {
                current = (MetadataViewModelBase)current.Parent;
            }

            return Context.Resolver.Get<IObjectLoader>(current.ObjectName);
        }

        private void Refresh(object obj)
        {
            RemoveChildrenFromModel();
            Children.Clear();
        }

        private async void LoadChildrenInternal()
        {
            try
            {
                IsBusy = true;

                if (!_wasLoaded)
                    await LoadChildren();
            }
            finally
            {
                _wasLoaded = true;
                IsBusy = false;
            }
        }
    }
}
