using System;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Application.ViewModel.Connections
{
    public sealed class MsSqlConnectionViewModel : ConnectionViewModelBase
    {
        public bool IntegratedSecurity
        {
            get { return bool.Parse(Model.Properties[ConnectionProperty.IntegratedSecurity]); }
            set
            {
                if (IntegratedSecurity.Equals(value))
                    return;

                Model.Properties[ConnectionProperty.IntegratedSecurity] = value.ToString();

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
    }
}
