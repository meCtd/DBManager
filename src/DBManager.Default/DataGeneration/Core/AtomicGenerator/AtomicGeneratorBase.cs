using System;
using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Default.DataGeneration.Core.AtomicGenerator
{
    public abstract class AtomicGeneratorBase
    {
        protected static Random _random = new Random();

        public abstract object Generate(long seed, DataGenerationColumnConfig columnConfig);
    }
}
