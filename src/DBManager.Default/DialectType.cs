using System.ComponentModel;

using Framework.Attributes;


namespace DBManager.Default
{
    public enum DialectType
    {
        [Hidden]
        Unknown,

        [Description("Microsoft SQL Server")]
        SqlServer,
        
        [Description("Microsoft Access")]
        Access,
    }
}
