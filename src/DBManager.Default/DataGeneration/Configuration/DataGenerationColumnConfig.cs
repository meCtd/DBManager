using System;
using System.Collections.Generic;
using DBManager.Default.DataType;

namespace DBManager.Default.DataGeneration.Configuration
{
    public class DataGenerationColumnConfig
    {
        public DataTypeFamily TypeFamily { get; set; }
        public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public object this[string propName] 
        {
            get => Properties[propName];
        }

        public DataGenerationColumnConfig(DataTypeFamily typeFamily)
        {
            TypeFamily = typeFamily;
        }
    }
}