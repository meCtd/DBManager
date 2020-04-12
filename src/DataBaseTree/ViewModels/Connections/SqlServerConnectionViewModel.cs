using System;

using DBManager.Default.DataBaseConnection;


namespace DBManager.Application.ViewModels.Connections
{
    public sealed class SqlServerConnectionViewModel : ConnectionViewModel
    {
        public bool IntegratedSecurity
        {
            get { return (bool)Model.Properties[ConnectionProperty.IntegratedSecurity]; }
            set
            {
                Model.Properties[ConnectionProperty.IntegratedSecurity] = value;

                UserId = value
                    ? Environment.UserName
                    : string.Empty;

                Password = string.Empty;

                OnPropertyChanged();
            }
        }

        public SqlServerConnectionViewModel(IConnectionData model) : base(model)
        {
        }

        protected override string ValidateColumn(string columnName)
        {
            switch (columnName)
            {
                case nameof(Password):
                {
                    if (IntegratedSecurity)
                        return string.Empty;

                    break;
                }
                case nameof(Port):
                {
                    if (!string.IsNullOrEmpty(Port) && !int.TryParse(Port, out _))
                        return NotValid;

                    break;
                }

                default: 
                    return base.ValidateColumn(columnName);
            }

            return string.Empty;
        }
    }
}
