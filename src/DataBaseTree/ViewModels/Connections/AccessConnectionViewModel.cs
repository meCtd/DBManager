using DBManager.Default.DataBaseConnection;

namespace DBManager.Application.ViewModels.Connections
{
    class AccessConnectionViewModel : ConnectionViewModel
    {
        public AccessConnectionViewModel(IConnectionData model)
            : base(model)
        {
        }

        protected override string ValidateColumn(string columnName)
        {
            switch (columnName)
            {
                case nameof(Password):
                    return string.Empty;
                default:
                    return base.ValidateColumn(columnName);
            }
        }
    }
}