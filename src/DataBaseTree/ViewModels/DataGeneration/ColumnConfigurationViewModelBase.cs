using DBManager.Application.ViewModels.General;
using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataType;

namespace DBManager.Application.ViewModels.DataGeneration
{
    abstract class ColumnConfigurationViewModelBase : ViewModelBase
    {
        protected DataGenConfig _config;

        public string ColumnName { get; set; }
        protected abstract DataTypeFamily TypeFamily { get; }

        public ColumnConfigurationViewModelBase(string columnName, DataGenConfig config)
        {
            _config = config;

            var columnConfig = new DataGenerationColumnConfig(TypeFamily);
            FillColumnConfigProperties(columnConfig);
            _config.ColumnConfigurations.Add(columnName, columnConfig);

            ColumnName = columnName.ToUpper();
        }

        protected abstract void FillColumnConfigProperties(DataGenerationColumnConfig config);
    }
}
