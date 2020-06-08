using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Default.DataGeneration.Core.AtomicGenerator
{
    class BoolValueGenerator : AtomicGeneratorBase
    {
        private const string TrueFactor = nameof(TrueFactor);
        private const string FalseFactor = nameof(FalseFactor);

        public override object Generate(long seed, DataGenerationColumnConfig columnConfig)
        {
            var range = (int)columnConfig[TrueFactor] + (int)columnConfig[FalseFactor];

            var value = _random.Next(range);

            return value < (int)columnConfig[TrueFactor];
        }
    }
}
