using System;
using System.Threading;
using System.Windows.Input;
using DBManager.Application.Framework.Providers;
using DBManager.Application.ViewModels.Connections;
using DBManager.Default;
using Prism.Commands;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels.Windows
{
    public class ConnectionWindowViewModel : ViewModelBase
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private ICommand _connectCommand;
        private ICommand _testConnectionCommand;
        private ICommand _cancelCommand;

        private DialectType _selectedBaseType;
        private ConnectionViewModelBase _connection;

        public DialectType SelectedBaseType
        {
            get { return _selectedBaseType; }
            set
            {
                if (SetProperty(ref _selectedBaseType, value))
                {
                    Conenction = CreateViewModel(SelectedBaseType);
                }
            }
        }

        public event EventHandler ConnectionSuccess;

        public ConnectionViewModelBase Conenction
        {
            get { return _connection; }
            set
            {
                SetProperty(ref _connection, value);
            }
        }

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new DelegateCommand(() => { }));

        public ICommand TestConnectionCommand => _testConnectionCommand ?? (_testConnectionCommand = new DelegateCommand(async () => await Conenction.Model.TestConnectionAsync(_tokenSource.Token)));

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(_tokenSource.Cancel));

        private ConnectionViewModelBase CreateViewModel(DialectType type)
        {
            var model = ConnectionProvider.Instance.CreateConnectionData(type);

            switch (model.Type)
            {
                case DialectType.MsSql:
                    return new MsSqlConnectionViewModel(model);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

