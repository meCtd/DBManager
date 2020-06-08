using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Default.DataGeneration.Core.AtomicGenerator
{
    class UnsupportedValueGenerator : AtomicGeneratorBase
    {
        public override object Generate(long seed, DataGenerationColumnConfig columnConfig)
        {
            return null;
        }
    }
}
