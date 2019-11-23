using DBManager.Default.Printers;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Printer
{
    public class MsSqlPrinterFactory : IPrinter
    {
        public string GetDefinition(DefinitionObject obj)
        {
            return obj.Definition = obj.Type == MetadataType.Table
                ? new MsSqlTablePrinter().GetDefinition(obj)
                : new MsSqlDefaultPrinter().GetDefinition(obj);
        }
    }
}
