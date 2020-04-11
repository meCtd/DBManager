using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.Connections;
using DBManager.Application.ViewModels.General;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loaders;

using Framework.EventArguments;

using Ninject;


namespace DBManager.Application.ViewModels.Windows
{
    public class ConnectionWindowViewModel : WindowViewModelBase
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private DialectType _selectedBaseType;

        private ICommand _connectCommand;
        private ICommand _testConnectionCommand;
        private ICommand _cancelCommand;

        private ConnectionViewModelBase _connection;

        private bool _isBusy;

        public DialectType SelectedBaseType
        {
            get { return _selectedBaseType; }
            set
            {
                if (SetProperty(ref _selectedBaseType, value))
                    Connection = CreateViewModel(SelectedBaseType);

            }
        }

        public event EventHandler<ArgumentEventArgs<string>> Connected;

        public ConnectionViewModelBase Connection
        {
            get => _connection;
            set => SetProperty(ref _connection, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand TestConnectionCommand => _testConnectionCommand ?? (_testConnectionCommand = new RelayCommand(s => ConnectAsync(TestConnection), CanConnect));

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand((s) => ConnectAsync(Connect), CanConnect));

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(s => _tokenSource.Cancel(), s => !IsBusy));

        public override string Header => "New connection";

        public ConnectionWindowViewModel()
        {
            SelectedBaseType = DialectType.SqlServer;
        }

        private Task TestConnection(bool connectResult)
        {
            var currentWindow = Resolver.Get<IWindowManager>().CurrentWindow;

            if (connectResult)
                MessageBox.Show(currentWindow, "Connection success!", "Connection", MessageBoxButton.OK, MessageBoxImage.Information);

            else
                MessageBox.Show(currentWindow, "Connection failed!", "Connection", MessageBoxButton.OK, MessageBoxImage.Warning);

            return Task.CompletedTask;
        }

        private async Task Connect(bool connectionResult)
        {
            if (connectionResult)
            {
                await RegisterConnection();
            }
        }

        private bool CanConnect(object s)
        {
            return Connection.IsValid && !IsBusy;
        }

        private async void ConnectAsync(Func<bool, Task> onConnect)
        {
            var currentWindow = Resolver.Get<IWindowManager>().CurrentWindow;
            try
            {
                IsBusy = true;

                await onConnect(await Connection.Model.TestConnectionAsync(_tokenSource.Token));


            }
            catch (Exception e)
            {
                MessageBox.Show(currentWindow, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private ConnectionViewModelBase CreateViewModel(DialectType type)
        {
            var model = (ConnectionData)Resolver.Get<IConnectionData>(SelectedBaseType.ToString());

            switch (type)
            {
                case DialectType.SqlServer:
                    return new SqlServerConnectionViewModel(model);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task RegisterConnection()
        {
            var loader = new ObjectLoader(Resolver.Get<IDialectComponent>(SelectedBaseType.ToString()), Connection.Model);

            await loader.LoadServerProperties(CancellationToken.None);

            var serverName = GetServerName(Connection.Model);

            Resolver.Rebind<IObjectLoader>()
                .ToConstant(loader)
                .Named(serverName);

            Connected?.Invoke(this, new ArgumentEventArgs<string>(serverName));
            Close();
        }

        private string GetServerName(ConnectionData data)
        {
            var builder = new StringBuilder();
            builder.Append(data.Host);

            if (!string.IsNullOrEmpty(data.Port))
                builder.Append($":{data.Port}");

            builder.Append($" ({data.UserId})");
            return builder.ToString();
        }

        public override void Dispose()
        {
            _tokenSource.Dispose();
            Connected = null;
            base.Dispose();
        }
    }
}