using DBManager.Default.Printers;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Printer
{
    internal class AccessPrinterFactory : IPrinter
    {
        public string GetTop100RowsQuery(DbObject obj)
        {
            return $"SELECT TOP 100 * FROM {obj.FullName.FullSchemaName}";
        }

        public string GetDefinition(DefinitionObject obj)
        {
            return null;
        }
    }
}
