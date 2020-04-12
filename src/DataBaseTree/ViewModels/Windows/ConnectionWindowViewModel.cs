using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using DBManager.Application.Loader;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.Connections;
using DBManager.Application.ViewModels.General;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;

using Framework.EventArguments;

using Ninject;


namespace DBManager.Application.ViewModels.Windows
{
    public class ConnectionWindowViewModel : WindowViewModelBase
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private DialectType _selectedDialect;

        private ICommand _connectCommand;
        private ICommand _testConnectionCommand;
        private ICommand _cancelCommand;

        private ConnectionViewModel _connection;

        private bool _isBusy;

        public DialectType SelectedDialect
        {
            get { return _selectedDialect; }
            set
            {
                if (SetProperty(ref _selectedDialect, value))
                    Connection = CreateViewModel(SelectedDialect);

            }
        }

        public IEnumerable<DialectType> AvailableDialects { get; }

        public event EventHandler<ArgumentEventArgs<(DialectType Dialect, string Name)>> Connected;

        public ConnectionViewModel Connection
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
            var dialects = Context.AvailableDialects.ToArray();
            AvailableDialects = dialects;
            SelectedDialect = dialects.FirstOrDefault();
        }

        private Task TestConnection(bool connectResult)
        {
            var currentWindow = Context.Resolver.Get<IWindowManager>().CurrentWindow;

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
            var currentWindow = Context.Resolver.Get<IWindowManager>().CurrentWindow;
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

        private ConnectionViewModel CreateViewModel(DialectType type)
        {
            var data = Context.Resolver
                .Get<IDialectComponent>(SelectedDialect.ToString())
                .CreateConnectionData();

            switch (type)
            {
                case DialectType.SqlServer:
                    return new SqlServerConnectionViewModel(data);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task RegisterConnection()
        {
            var component = Context.Resolver.Get<IDialectComponent>(SelectedDialect.ToString());

            var loader = new ObjectLoader(component, Connection.Model);
            await loader.LoadServerProperties(CancellationToken.None);

            var serverName = GetServerName(Connection.Model);

            Context.Resolver.Rebind<IObjectLoader>()
                .ToConstant(loader)
                .Named(serverName);

            Connected?.Invoke(this, new ArgumentEventArgs<(DialectType Dialect, string Name)>((Dialect: SelectedDialect, Name: serverName)));
            Close();
        }

        private string GetServerName(IConnectionData data)
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