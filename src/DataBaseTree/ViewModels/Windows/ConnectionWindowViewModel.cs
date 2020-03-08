using System;
using System.Threading;
using System.Windows.Input;
using DBManager.Application.Providers;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.Connections;
using DBManager.Application.ViewModels.General;
using DBManager.Default;

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
                {
                    Connection = CreateViewModel(SelectedBaseType);
                }
            }
        }

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

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand((s) => Connect()));

        public override string Header => "New connection";

        public ConnectionWindowViewModel()
        {
            SelectedBaseType = DialectType.MsSql;
        }

        private void Connect()
        {
            throw new NotImplementedException();
        }

        public ICommand TestConnectionCommand => _testConnectionCommand ?? (_testConnectionCommand = new RelayCommand(async (s) => await Connection.Model.TestConnectionAsync(_tokenSource.Token)));

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(s => _tokenSource.Cancel()));

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


    }
}

