﻿using System.Collections.Generic;
using System.ComponentModel;

using DBManager.Application.ViewModels.General;

using DBManager.Default.DataBaseConnection;


namespace DBManager.Application.ViewModels.Connections
{
    public class ConnectionViewModel : ViewModelBase, IDataErrorInfo
    {
        protected const string IsRequired = "Field is required";
        protected const string NotValid = "Value is not valid";

        private bool _isValid;
        private readonly HashSet<string> _invalidColumns = new HashSet<string>();

        public IConnectionData Model { get; }

        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        public string Host
        {
            get { return Model.Host; }
            set
            {
                Model.Host = value;
                OnPropertyChanged();
            }
        }

        public string Port
        {
            get { return Model.Port; }
            set
            {
                Model.Port = value;
                OnPropertyChanged();
            }
        }

        public string InitialCatalog
        {
            get { return Model.InitialCatalog; }
            set
            {
                Model.InitialCatalog = value;
                OnPropertyChanged();
            }
        }

        public string UserId
        {
            get { return Model.UserId; }
            set
            {
                Model.UserId = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return Model.Password; }
            set
            {
                Model.Password = value;
                OnPropertyChanged();
            }
        }

        public ConnectionViewModel(IConnectionData model)
        {
            Model = model;
            Host = @".\SqlExpress";
        }

        protected virtual string ValidateColumn(string columnName)
        {
            switch (columnName)
            {
                case nameof(Host):
                    if (string.IsNullOrEmpty(Host))
                        return IsRequired;
                    break;

                case nameof(Port):
                    if (string.IsNullOrEmpty(Port))
                        return IsRequired;

                    if (!int.TryParse(Port, out _))
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

