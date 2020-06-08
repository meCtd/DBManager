using System;
using System.Collections.Generic;
using DBManager.Application.ViewModels.General;
using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataType;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class ConfigurationViewModelFactory
    {
        #region Singleton
        private static ConfigurationViewModelFactory _instance;

        public static ConfigurationViewModelFactory Instance => _instance ?? (_instance = new ConfigurationViewModelFactory());

        private ConfigurationViewModelFactory()
        { }
        #endregion

        private Dictionary<DataTypeFamily, Func<string, DataGenConfig, ViewModelBase>> _dataTypeFamilyToViewModelMap = new Dictionary<DataTypeFamily, Func<string, DataGenConfig, ViewModelBase>>()
        {
            [DataTypeFamily.Integer] = (name, config) => new IntegerConfigurationViewModel(name, config),
            [DataTypeFamily.Float] = (name, config) => new FloatConfigurationViewModel(name, config),
            [DataTypeFamily.String] = (name, config) => new StringConfigurationViewModel(name, config),
            [DataTypeFamily.DateTime] = (name, config) => new DateTimeConfigurationViewModel(name, config),
            [DataTypeFamily.Boolean] = (name, config) => new BoolConfigurationViewModel(name, config),
        };

        internal ViewModelBase CreateViewModel(DataTypeFamily typeFamily, string columnName, DataGenConfig config)
        {
            return _dataTypeFamilyToViewModelMap[typeFamily](columnName, config);
        }
    }
}
