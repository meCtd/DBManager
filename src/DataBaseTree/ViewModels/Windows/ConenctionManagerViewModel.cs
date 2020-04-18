using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;

using Framework.Extensions;
using Ninject;


namespace DBManager.Application.ViewModels.Windows
{
    public class ConnectionManagerViewModel : ViewModelBase
    {
        private readonly TreeViewModel _metadataTree;

        private ICommand _disconnectCommand;
        private ICommand _connectCommand;

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand(s => Connect()));

        public ICommand DisconnectCommand => _disconnectCommand ?? (_disconnectCommand = new RelayCommand<MetadataViewModelBase>(Disconnect, s => _metadataTree.RootItems.Count > 0));

        public ConnectionManagerViewModel(TreeViewModel metadataTree)
        {
            _metadataTree = metadataTree;
        }

        private void Connect()
        {
            var viewModel = new ConnectionWindowViewModel();
            viewModel.OnConnected(OnConnectedAsync);

            Context.Resolver
                .Get<IWindowManager>()
                .ShowWindow(viewModel);
        }

        private void Disconnect(MetadataViewModelBase obj)
        {
            var root = obj.Root;
            _metadataTree.RootItems.Remove(root);

            //Context.Resolver
            //   .GetBindings(typeof(IObjectLoader))
            //   .Where(s => s.Metadata.Name?.Equals(root.Name) ?? false)
            //   .ForEach(Context.Resolver.RemoveBinding);
        }

        private async Task OnConnectedAsync(IConnectionData data)
        {
            var loader = Context.Resolver.Get<IDialectComponent>(data.Dialect.ToString()).Loader;

            var server = await loader.LoadServerAsync(new LoadingContext(data, CancellationToken.None));
            //Context.Resolver.Bind<IObjectLoader>()
            //    .ToConstant(loader)
            //    .Named(serverName);

            if (_metadataTree.RootItems.Cast<MetadataViewModelBase>().Any(s => s.Name.Equals(server.Name)))
                return;

            _metadataTree.RootItems.Add(new ServerViewModel(server));
        }
    }
}
