using System;
using System.Collections.Generic;

namespace DBManager.Default.DataGeneration.Configuration
{
    public class DataGenerationColumnConfig
    {
        public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public object this[string propName] 
        {
            get => Properties[propName];
        }
    }
}