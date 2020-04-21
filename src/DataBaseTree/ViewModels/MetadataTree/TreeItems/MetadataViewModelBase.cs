using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Default;
using DBManager.Default.Tree;
using Framework.Extensions;
using Framework.Utils;
using Ninject;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public abstract class MetadataViewModelBase : TreeViewItemViewModelBase
    {
        private readonly Guid _guid = Guid.NewGuid();

        private bool _isBusy;
        private bool _wasLoaded;
        private ICommand _refreshCommand;

        public event EventHandler Loaded;

        protected virtual IDialectComponent Components => Root.Components;

        public new MetadataViewModelBase Parent => base.Parent as MetadataViewModelBase;

        public abstract MetadataType Type { get; }

        public virtual DialectType Dialect => Root.Dialect;

        public MetadataViewModelBase Root => this.GetParent(s => s.Parent);

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
                if (args.New)
                {
                    LoadChildrenAsync();
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

        public Task LoadChildrenAsync()
        {
            return Context.Resolver.Get<IAsyncAwaiter>().AwaitAsync(_guid.ToString(), LoadChildrenAsyncInternal);
        }

        private async Task LoadChildrenAsyncInternal()
        {
            if (_wasLoaded)
                return;
            try
            {
                IsBusy = true;
                Context.Resolver.Get<IWindowManager>().RunOnUi(Children.Clear);
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
