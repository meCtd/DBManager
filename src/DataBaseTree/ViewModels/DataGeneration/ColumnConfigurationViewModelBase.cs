using DBManager.Application.ViewModels.General;
using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Application.ViewModels.DataGeneration
{
    abstract class ColumnConfigurationViewModelBase : ViewModelBase
    {
        protected DataGenConfig _config;

        public string ColumnName { get; set; }

        public ColumnConfigurationViewModelBase(string columnName, DataGenConfig config)
        {
            _config = config;

            var columnConfig = new DataGenerationColumnConfig();
            FillColumnConfigProperties(columnConfig);
            _config.ColumnConfigurations.Add(columnName, columnConfig);

            ColumnName = columnName.ToUpper();
        }

        protected abstract void FillColumnConfigProperties(DataGenerationColumnConfig config);
    }
}
