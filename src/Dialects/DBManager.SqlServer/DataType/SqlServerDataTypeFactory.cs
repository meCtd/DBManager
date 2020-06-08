using System.Collections.Generic;

using DBManager.Default.DataType;

namespace DBManager.SqlServer.DataType
{
    class SqlServerDataTypeFactory : DataTypeFactory
    {
        protected override Dictionary<string, TypeDescriptor> TypeDescriptorsMap { get; } = new Dictionary<string, TypeDescriptor>()
        {
            ["bit"] = new TypeDescriptor("BIT", DataTypeFamily.Boolean),
            ["int"] = new TypeDescriptor("INT", DataTypeFamily.Integer),
            ["float"] = new TypeDescriptor("FLOAT", DataTypeFamily.Float),
            ["nvarchar"] = new TypeDescriptor("NVARCHAR", DataTypeFamily.String),
            ["datetime2"] = new TypeDescriptor("DATETIME2", DataTypeFamily.DateTime),
        };
    }
}
