using System;

using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Default.DataGeneration.Core.AtomicGenerator
{
    class IntegerValueGenerator : AtomicGeneratorBase
    {
        private const string MinValue = nameof(MinValue);
        private const string MaxValue = nameof(MaxValue);

        public override object Generate(long seed, DataGenerationColumnConfig columnConfig)
        {
            return _random.Next((int)columnConfig[MinValue], (int)columnConfig[MaxValue]);
        }
    }
}
