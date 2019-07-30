using System.Collections.Generic;
using System.Windows.Input;
using DBManager.Default.DataBaseConnection;
using Prism.Mvvm;

namespace DBManager.Application.ViewModel.ConnectionViewModel
{
    public abstract class ConnectionViewModelBase : BindableBase
    {
        private readonly ConnectionData _model;

        private readonly Dictionary<string, string> _advancedProperties = new Dictionary<string, string>();

        public string Server
        {
            get { return _model.Server; }
            set
            {
                _model.Server = value;
                RaisePropertyChanged();
            }
        }

        public string Port
        {
            get { return _model.Port; }
            set
            {
                _model.Port = value;
                RaisePropertyChanged();
            }
        }

        public string InitialCatalog
        {
            get { return _model.InitialCatalog; }
            set
            {
                _model.InitialCatalog = value;
                RaisePropertyChanged();
            }
        }

        public string UserId
        {
            get { return _model.UserId; }
            set
            {
                _model.UserId = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get { return _model.Password; }
            set
            {
                _model.Password = value;
                RaisePropertyChanged();
            }
        }


        protected ConnectionViewModelBase(ConnectionData model)
        {
            _model = model;
        }
    }
}

