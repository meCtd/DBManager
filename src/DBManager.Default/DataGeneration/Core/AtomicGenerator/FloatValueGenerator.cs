
using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Default.DataGeneration.Core.AtomicGenerator
{
    class FloatValueGenerator : AtomicGeneratorBase
    {
        private const string MinValue = nameof(MinValue);
        private const string MaxValue = nameof(MaxValue);

        public override object Generate(long seed, DataGenerationColumnConfig columnConfig)
        {
            var min = (double)columnConfig[MinValue];
            var max = (double)columnConfig[MaxValue];

            return _random.NextDouble() * (max - min) + min;
        }
    }
}
