using System;
using System.Collections.Generic;
using System.Linq;

using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataGeneration.Configuration.Enum;
using DBManager.Default.DataType;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class FloatConfigurationViewModel : ColumnConfigurationViewModelBase
    {
        protected override DataTypeFamily TypeFamily => DataTypeFamily.Float;

        public double MinValue
        {
            get => (double)_config[ColumnName][nameof(MinValue)];
            set
            {
                _config[ColumnName].Properties[nameof(MinValue)] = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }
        public double MaxValue
        {
            get => (double)_config[ColumnName][nameof(MaxValue)];
            set
            {
                _config[ColumnName].Properties[nameof(MaxValue)] = value;
                OnPropertyChanged(nameof(MaxValue));
            }
        }

        public int Precision
        {
            get => (int)_config[ColumnName][nameof(Precision)];
            set
            {
                _config[ColumnName].Properties[nameof(Precision)] = value;
                OnPropertyChanged(nameof(Precision));
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

        public FloatConfigurationViewModel(string columnName, DataGenConfig config)
            : base(columnName, config)
        {
        }

        protected override void FillColumnConfigProperties(DataGenerationColumnConfig config)
        {
            config.Properties.Add(nameof(MinValue), 0.0);
            config.Properties.Add(nameof(MaxValue), 1.0);

            config.Properties.Add(nameof(Precision), 1);

            config.Properties.Add(nameof(Distribution), Distribution.Uniform);
        }
    }
}
