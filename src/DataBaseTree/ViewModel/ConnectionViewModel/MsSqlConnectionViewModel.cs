using System;
using System.Windows;
using System.Windows.Input;
using DBManager.Application.Framework;
using DBManager.Application.View;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Application.ViewModel.ConnectionViewModel
{
    public sealed class MsSqlConnectionViewModel : ConnectionViewModelBase
    {
        private bool _integratedSecurity;
        
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

        public MsSqlConnectionViewModel(ConnectionData model) : base(model)
        {

        }
    }
}
