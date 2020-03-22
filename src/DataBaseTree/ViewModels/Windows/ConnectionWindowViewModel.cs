using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DBManager.Application.Providers;
using DBManager.Application.Providers.Abstract;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.Connections;
using DBManager.Application.ViewModels.General;
using DBManager.Default;
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
            SelectedBaseType = DialectType.MsSql;
        }

        private void TestConnection(bool connectResult)
        {
            var currentWindow = Resolver.Get<IWindowManager>().CurrentWindow;

            if (connectResult)
                MessageBox.Show(currentWindow, "Connection success!", "Connection", MessageBoxButton.OK, MessageBoxImage.Information);

            else
                MessageBox.Show(currentWindow, "Connection failed!", "Connection", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Connect(bool connectionResult)
        {
            IsBusy = true;

            if (connectionResult)
            {
                RegisterConnection();
            }
        }

        private bool CanConnect(object s)
        {
            return Connection.IsValid && !IsBusy;
        }

        private async void ConnectAsync(Action<bool> onConnect)
        {
            var currentWindow = Resolver.Get<IWindowManager>().CurrentWindow;
            try
            {
                IsBusy = true;

                onConnect(await Connection.Model.TestConnectionAsync(_tokenSource.Token));


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
            var model = Resolver.Get<ConnectionProvider>().ProvideConnection(type);

            switch (type)
            {
                case DialectType.MsSql:
                    return new MsSqlConnectionViewModel(model);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RegisterConnection()
        {
            var component = Resolver.Get<IComponentProvider>().ProvideComponent(SelectedBaseType);

            Resolver.Bind<IDialectComponent>()
                .ToConstant(component)
                .InSingletonScope();
            //.Named(); 

            Resolver.Bind<IObjectLoader>()
                .To<ObjectLoader>()
                .WithConstructorArgument(component)
                .WithConstructorArgument(Connection.Model);
            //.Named(); 

            Connected?.Invoke(this, new ArgumentEventArgs<string>(Connection.Model.GetServerName()));
            Close();
        }

        public override void Dispose()
        {
            _tokenSource.Dispose();
            Connected = null;
            base.Dispose();
        }
    }
}