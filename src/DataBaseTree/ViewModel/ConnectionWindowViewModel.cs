using System;
using System.Windows.Input;
using DBManager.Application.ViewModel.ConnectionViewModel;
using DBManager.Default;
using Prism.Commands;
using Prism.Mvvm;

namespace DBManager.Application.ViewModel
{
    public class ConnectionWindowViewModel : BindableBase
    {
        private readonly ICommand _createConnectionCommand;
        private readonly ICommand _testConnectionCommand;

        

        private ConnectionViewModelBase _connection;
        private DialectType _selectedBaseType;


        public DialectType SelectedBaseType
        {
            get { return _selectedBaseType; }
            set
            {
                if (SetProperty(ref _selectedBaseType, value))
                {
                    Conenction = Con
                }
            }
        }

        public ConnectionViewModelBase Conenction
        {
            get { return _connectionData; }
            set
            {
                SetProperty(ref _connectionData, value);
            }
        }


        public ConnectionWindowViewModel()
        {
            ConnectionData = Co
        }


        //TODO: Implement
        public ICommand CreateConnectionCommand => _createConnectionCommand ?? (_createConnectionCommand = new DelegateCommand())

        public ICommand TestConnectionCommand => _testConnectionCommand;
    }
}

