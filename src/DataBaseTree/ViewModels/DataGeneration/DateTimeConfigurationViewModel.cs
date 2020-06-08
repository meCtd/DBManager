using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataType;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class DateTimeConfigurationViewModel : ColumnConfigurationViewModelBase
    {
        protected override DataTypeFamily TypeFamily => DataTypeFamily.DateTime;

        public DateTimeConfigurationViewModel(string columnName, DataGenConfig config)
            : base(columnName, config)
        {
        }

        protected override void FillColumnConfigProperties(DataGenerationColumnConfig config)
        {

        }
    }
}
