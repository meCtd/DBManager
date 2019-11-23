using DBManager.Default.Printers;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Printer
{
    public class MsSqlDefaultPrinter : IPrinter
    {
        public string GetDefinition(DefinitionObject obj)
        {
            return $"CREATE {obj.Type.ToString().ToUpper()} {SqlServerNormalizer.Instance.Quote(obj.Name)}";
        }
    }
}
