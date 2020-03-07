using System;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Application.ViewModels.Connections
{
    public sealed class MsSqlConnectionViewModel : ConnectionViewModelBase
    {
        public bool IntegratedSecurity
        {
            get { return (bool) Model.Properties[ConnectionProperty.IntegratedSecurity]; }
            set
            {
                if (IntegratedSecurity.Equals(value))
                    return;

                Model.Properties[ConnectionProperty.IntegratedSecurity] = value;

                if (value)
                {
                    UserId = Environment.UserName;
                    Password = string.Empty;
                }
            }
        }

        public MsSqlConnectionViewModel(ConnectionData model) : base(model)
        {
        }

        protected override string ValidateColumn(string columnName)
        {
            if (IntegratedSecurity && columnName == nameof(Password))
                return string.Empty;

            return base.ValidateColumn(columnName);
        }
    }
}
