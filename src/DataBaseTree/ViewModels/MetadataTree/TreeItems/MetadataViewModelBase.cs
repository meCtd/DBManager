using System;
using System.Threading.Tasks;
using System.Windows.Input;

using DBManager.Application.Utils;

using DBManager.Default;
using DBManager.Default.Tree;
using Framework.Extensions;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public abstract class MetadataViewModelBase : TreeViewItemViewModelBase
    {
        private bool _isBusy;
        private bool _wasLoaded;
        private ICommand _refreshCommand;
        private IDialectComponent _components;
        private DialectType? _dialect;


        public event EventHandler Loaded;

        protected virtual IDialectComponent Components => _components ?? (_components = Root.Components);

        public new MetadataViewModelBase Parent => base.Parent as MetadataViewModelBase;

        public abstract string Name { get; }

        public abstract MetadataType Type { get; }

        public MetadataViewModelBase Root => this.GetParent(s => s.Parent);
        
        public virtual DialectType Dialect => (_dialect ?? (_dialect = Root.Dialect)).Value;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));

        protected MetadataViewModelBase(MetadataViewModelBase parent, bool canHaveChildren) : base(parent, canHaveChildren)
        {
            ExpandChanged += (sender, args) =>
            {
                if (args.New && !_wasLoaded)
                {
                    LoadChildrenInternal();
                }
            };

            Refreshed += (s, e) => _wasLoaded = false;
        }

        protected abstract void RemoveChildrenFromModel();
        protected abstract Task LoadChildren();

        private void Refresh(object obj)
        {
            RemoveChildrenFromModel();
            Refresh();
        }

        private async void LoadChildrenInternal()
        {
            try
            {
                IsBusy = true;

                await LoadChildren();
            }
            finally
            {
                _wasLoaded = true;
                IsBusy = false;

                Loaded?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
