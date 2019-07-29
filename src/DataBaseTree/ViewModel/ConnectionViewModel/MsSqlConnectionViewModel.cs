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
        private int _connectionTimeout = 5;

        private bool _integratedSecurity;

        private bool _pooling = true;


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



        public MsSqlConnectionViewModel(ConnectionData model) : base(model)
        {

        }
    }
}
