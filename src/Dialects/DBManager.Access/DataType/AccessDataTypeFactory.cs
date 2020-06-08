using System.Collections.Generic;

using DBManager.Default.DataType;

namespace DBManager.Access.DataType
{
    class AccessDataTypeFactory : DataTypeFactory
    {
        protected override Dictionary<string, TypeDescriptor> TypeDescriptorsMap { get; } = new Dictionary<string, TypeDescriptor>()
        {

        };
    }
}
