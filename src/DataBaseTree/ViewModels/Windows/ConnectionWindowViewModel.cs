using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.Connections;
using DBManager.Application.ViewModels.General;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;

using Ninject;


namespace DBManager.Application.ViewModels.Windows
{
    public class ConnectionWindowViewModel : WindowViewModelBase
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private readonly List<Func<IConnectionData, Task>> _handlers = new List<Func<IConnectionData, Task>>();

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

        public void OnConnected(Func<IConnectionData, Task> handler)
        {
            _handlers.Add(handler);
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
            await Task.WhenAll(_handlers.Select(s => s.Invoke(Connection.Model)));
            Close();
        }

        public override void Dispose()
        {
            _tokenSource.Dispose();
            _handlers.Clear();
            base.Dispose();
        }
    }
}