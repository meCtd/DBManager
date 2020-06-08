using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataType;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class StringConfigurationViewModel : ColumnConfigurationViewModelBase
    {
        protected override DataTypeFamily TypeFamily => DataTypeFamily.String;

        public StringConfigurationViewModel(string columnName, DataGenConfig config) 
            : base(columnName,config)
        {
        }

        protected override void FillColumnConfigProperties(DataGenerationColumnConfig config)
        {

        }
    }
}
