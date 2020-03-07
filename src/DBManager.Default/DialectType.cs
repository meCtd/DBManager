using System.ComponentModel;

namespace DBManager.Default
{
    public enum DialectType
    {
        Unknown,

        [Description("Microsoft SQL Server")]
        MsSql
    }
}
