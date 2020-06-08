using System.Collections.Generic;

using DBManager.Default.DataGeneration.Core.AtomicGenerator;
using DBManager.Default.DataType;

namespace DBManager.Default.DataGeneration.Core
{
    class AtomicGeneratorFactory
    {
        private static Dictionary<DataTypeFamily, AtomicGeneratorBase> _typeFamilyToGeneratorMap = new Dictionary<DataTypeFamily, AtomicGeneratorBase>()
        {
            [DataTypeFamily.Integer] = new IntegerValueGenerator(),
            [DataTypeFamily.Float] = new FloatValueGenerator(),
            [DataTypeFamily.Boolean] = new BoolValueGenerator(),
        };

        public static AtomicGeneratorBase GetAtomicGenerator(DataTypeFamily typeFamily)
        {
            if (_typeFamilyToGeneratorMap.TryGetValue(typeFamily, out var value))
                return value;

            return new UnsupportedValueGenerator();
        }
    }
}
