using System;
using System.Collections.Generic;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.DataGeneration.Configuration
{
    public class DataGenConfig
    {
        public Table Scope { get; set; }

        public int RowCount { get; set; } = 1;
        public long Seed { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        public Dictionary<string, DataGenerationColumnConfig> ColumnConfigurations { get; set; }

        public DataGenerationColumnConfig this[string columnName]
        {
            get => ColumnConfigurations[columnName];
        }

        public DataGenConfig(Table scope)
        {
            Scope = scope;
            ColumnConfigurations = new Dictionary<string, DataGenerationColumnConfig>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
