using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Default.DataGeneration.Core
{
    class DataRecordCreator
    {
        public void FillDataRecord(DataRow row, DataGenConfig config)
        {
            foreach (var item in config.ColumnConfigurations)
            {
                row[item.Key] = AtomicGeneratorFactory.GetAtomicGenerator(item.Value.TypeFamily).Generate(config.Seed, item.Value);
            }
        }
    }
}
