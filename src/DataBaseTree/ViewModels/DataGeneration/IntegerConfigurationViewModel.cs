using System;
using System.Collections.Generic;
using System.Linq;
using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataGeneration.Configuration.Enum;
using DBManager.Default.DataType;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class IntegerConfigurationViewModel : ColumnConfigurationViewModelBase
    {
        protected override DataTypeFamily TypeFamily => DataTypeFamily.Integer;

        public int MinValue 
        { 
            get => (int)_config[ColumnName][nameof(MinValue)];
            set
            {
                _config[ColumnName].Properties[nameof(MinValue)] = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }
        public int MaxValue
        {
            get => (int)_config[ColumnName][nameof(MaxValue)];
            set
            {
                _config[ColumnName].Properties[nameof(MaxValue)] = value;
                OnPropertyChanged(nameof(MaxValue));
            }
        }

        public IEnumerable<Distribution> AllDistributions => Enum.GetValues(typeof(Distribution)).Cast<Distribution>();
        public Distribution Distribution
        {
            get => (Distribution)_config[ColumnName][nameof(Distribution)];
            set
            {
                _config[ColumnName].Properties[nameof(Distribution)] = value;
                OnPropertyChanged(nameof(Distribution));
            }
        }

        public IntegerConfigurationViewModel(string columnName, DataGenConfig config)
            : base(columnName, config)
        {
        }

        protected override void FillColumnConfigProperties(DataGenerationColumnConfig config)
        {
            config.Properties.Add(nameof(MinValue), 0);
            config.Properties.Add(nameof(MaxValue), 10);

            config.Properties.Add(nameof(Distribution), Distribution.Uniform);
        }
    }
}
