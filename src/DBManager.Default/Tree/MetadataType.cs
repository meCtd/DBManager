using System;

namespace DBManager.Default.Tree
{
    [Flags]
    public enum MetadataType
    {
        None = 0,
        Server = 1 << 1,
        Database = 1 << 2,
        Schema = 1 << 3,
        Table = 1 << 4,
        View = 1 << 5,
        Function = 1 << 6,
        Procedure = 1 << 7,
        Constraint = 1 << 8,
        Column = 1 << 9,
        Trigger = 1 << 10,
        Parameter = 1 << 11,
        Index = 1 << 12,
        Type = 1 << 13,
        DataObject = MetadataType.View | MetadataType.Table
    }
}
