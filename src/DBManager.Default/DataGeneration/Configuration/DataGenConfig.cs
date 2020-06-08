using System;
using System.Collections.Generic;

namespace DBManager.Default.DataGeneration.Configuration
{
    public class DataGenConfig
    {
        public int RowCount { get; set; } = 1;
        public long Seed { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        public Dictionary<string, DataGenerationColumnConfig> ColumnConfigurations { get; set; }

        public DataGenerationColumnConfig this[string columnName]
        {
            get => ColumnConfigurations[columnName];
        }

        public DataGenConfig()
        {
            ColumnConfigurations = new Dictionary<string, DataGenerationColumnConfig>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
