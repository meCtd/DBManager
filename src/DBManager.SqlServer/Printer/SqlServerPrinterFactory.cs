using DBManager.Default.Printers;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Printer
{
    internal class SqlServerPrinterFactory : IPrinter
    {
        public string GetDefinition(DefinitionObject obj)
        {
            return obj.Definition = obj.Type == MetadataType.Table
                ? new SqlServerTablePrinter().GetDefinition(obj)
                : new SqlServerDefaultPrinter().GetDefinition(obj);
        }
    }
}
