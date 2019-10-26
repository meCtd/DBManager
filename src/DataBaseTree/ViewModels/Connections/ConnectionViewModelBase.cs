using System.Collections.Generic;
using System.ComponentModel;
using DBManager.Default.DataBaseConnection;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels.Connections
{
    public abstract class ConnectionViewModelBase : BindableBase, IDataErrorInfo
    {
        protected const string IsRequired = "Field is required";
        protected const string NotValid = "Value is not valid";

        private bool _isValid;
        private bool _isInProgress;
        private readonly HashSet<string> _invalidColumns = new HashSet<string>();

        public ConnectionData Model { get; }

        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        public bool IsInProgress
        {
            get { return _isInProgress; }
            set { SetProperty(ref _isInProgress, value); }
        }

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

        protected ConnectionViewModelBase(ConnectionData model)
        {
            Model = model;
        }


        protected virtual string ValidateColumn(string columnName)
        {
            switch (columnName)
            {
                case nameof(Server):
                    if (string.IsNullOrEmpty(Server))
                        return IsRequired;
                    break;

                case nameof(Port):
                    if (string.IsNullOrEmpty(Password))
                        return IsRequired;

                    if (!bool.TryParse(Port, out _))
                        return NotValid;
                    break;

                case nameof(UserId):
                    if (string.IsNullOrEmpty(UserId))
                        return IsRequired;
                    break;

                case nameof(Password):
                    if (string.IsNullOrEmpty(Password))
                        return IsRequired;
                    break;
            }

            return string.Empty;
        }

        #region IDataErrorInfoImpl

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = ValidateColumn(columnName);

                if (!string.IsNullOrEmpty(error))
                    _invalidColumns.Add(columnName);

                else
                    _invalidColumns.Remove(columnName);

                IsValid = _invalidColumns.Count == 0;
                return error;
            }
        }

        string IDataErrorInfo.Error => null;

        #endregion
    }
}

