using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class BoolConfigurationViewModel : ColumnConfigurationViewModelBase
    {
        public int TrueFactor
        {
            get => (int)_config[ColumnName][nameof(TrueFactor)];
            set
            {
                _config[ColumnName].Properties[nameof(TrueFactor)] = value;
                OnPropertyChanged(nameof(TrueFactor));
            }
        }
        public int FalseFactor
        {
            get => (int)_config[ColumnName][nameof(FalseFactor)];
            set
            {
                _config[ColumnName].Properties[nameof(FalseFactor)] = value;
                OnPropertyChanged(nameof(FalseFactor));
            }
        }

        public BoolConfigurationViewModel(string columnName, DataGenConfig config)
            : base(columnName, config)
        {
        }

        protected override void FillColumnConfigProperties(DataGenerationColumnConfig config)
        {
            config.Properties.Add(nameof(TrueFactor), 1);
            config.Properties.Add(nameof(FalseFactor), 1);
        }
    }
}
