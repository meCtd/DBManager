using System;
using System.Windows;
using System.Windows.Input;
using DataBaseTree.Framework;
using DataBaseTree.Model.DataBaseConnection;
using DataBaseTree.View;

namespace DataBaseTree.ViewModel.ConnectionViewModel
{
    public sealed class MsSqlConnectionViewModel : BaseConnectionViewModel
    {
        private MsSqlServer _model;

        #region Fields

        private int _connectionTimeout = 5;

        private bool _integratedSecurity;

        private bool _pooling = true;

        #endregion

        #region Properties

        public int ConnectionTimeout
        {
            get { return _connectionTimeout; }
            set { SetProperty(ref _connectionTimeout, value); }
        }

        public bool IntegratedSecurity
        {
            get { return _integratedSecurity; }
            set
            {
                if (SetProperty(ref _integratedSecurity, value))
                {
                    UserId = value ? Environment.UserName : string.Empty;
                    Password = string.Empty;
                }
            }
        }

        public bool Pooling
        {
            get { return _pooling; }
            set { SetProperty(ref _pooling, value); }
        }

        #endregion


        public MsSqlConnectionViewModel()
        {
            _model = new MsSqlServer();
        }

        private async void CreateConnection(ConnectionWindow window)
        {
            IsBusy = true;
            MsSqlServer server = new MsSqlServer()
            {
                Server = _server,
                Port = _port,
                InitialCatalog = _initialCatalog,
                UserId = _userId,
                Password = _password,
                IntegratedSecurity = _integratedSecurity,
                ConnectionTimeout = _connectionTimeout,
                Pooling = _pooling
            };
            if (await server.TestConnectionAsync())
            {
                IsBusy = false;
                Connection = server;
                window.DialogResult = true;
                window.Close();
            }
            else
            {
                IsBusy = false;
                MessageBox.Show("Connection failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void TestConnection()
        {
            IsBusy = true;
            MsSqlServer test = new MsSqlServer()
            {
                Server = _server,
                Port = _port,
                InitialCatalog = _initialCatalog,
                UserId = _userId,
                Password = _password,
                IntegratedSecurity = _integratedSecurity,
                ConnectionTimeout = _connectionTimeout,
                Pooling = _pooling
            };

            if (await test.TestConnectionAsync())
            {
                IsBusy = false;

                MessageBox.Show("Connection succsessful!", "Test Connection", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                IsBusy = false;

                MessageBox.Show("Connection failed!", "Test Connection", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private bool IsValid(ConnectionWindow window)
        {
            bool inputSuccess = !string.IsNullOrWhiteSpace(Server);

            if (!IntegratedSecurity)
            {
                inputSuccess = inputSuccess && !(string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password));
            }

            return inputSuccess && !IsBusy;
        }

        #region Commands

        #region CreateConnectionCommand

        private RelayCommand<ConnectionWindow> _createConnectionCommand;

        public override ICommand CreateConnectionCommand
        {
            get
            {
                return _createConnectionCommand ?? (_createConnectionCommand = new RelayCommand<ConnectionWindow>(
                           CreateConnection,
                           IsValid));
            }
        }

        #endregion

        #region TestConnectionCommand

        private RelayCommand _testConnectionCommand;

        public override ICommand TestConnectionCommand
        {
            get
            {
                return _testConnectionCommand ?? (_testConnectionCommand = new RelayCommand(
                            TestConnection,
                            IsValid(null)));
            }
        }

        #endregion

        #endregion

    }
}
