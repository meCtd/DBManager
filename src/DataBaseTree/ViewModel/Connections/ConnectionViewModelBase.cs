using System.Collections.Generic;
using System.ComponentModel;
using DBManager.Default.DataBaseConnection;
using Prism.Mvvm;

namespace DBManager.Application.ViewModel.Connections
{
    public abstract class ConnectionViewModelBase : BindableBase, IDataErrorInfo
    {
        public ConnectionData Model { get; }

        private readonly Dictionary<string, string> _advancedProperties = new Dictionary<string, string>();

        public string Server
        {
            get { return Model.Server; }
            set
            {
                Model.Server = value;
                RaisePropertyChanged();
            }
        }

        public string Port
        {
            get { return Model.Port ?? Model.DefaultPort; }
            set
            {
                Model.Port = value;
                RaisePropertyChanged();
            }
        }

        public string InitialCatalog
        {
            get { return Model.InitialCatalog; }
            set
            {
                Model.InitialCatalog = value;
                RaisePropertyChanged();
            }
        }

        public string UserId
        {
            get { return Model.UserId; }
            set
            {
                Model.UserId = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get { return Model.Password; }
            set
            {
                Model.Password = value;
                RaisePropertyChanged();
            }
        }

        public bool IsValid;

        protected ConnectionViewModelBase(ConnectionData model)
        {
            Model = model;
        }

        public string this[string columnName] => throw new System.NotImplementedException();

        public string Error { get; }
    }
}

