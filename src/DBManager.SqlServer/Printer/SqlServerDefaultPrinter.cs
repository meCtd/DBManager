using DBManager.Default.Printers;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Printer
{
    internal class SqlServerDefaultPrinter : IPrinter
    {
        public string GetDefinition(DefinitionObject obj)
        {
            return $"CREATE {obj.Type.ToString().ToUpper()} {SqlServerComponent.SqlNormalizer.Quote(obj.Name)}";
        }
    }
}
