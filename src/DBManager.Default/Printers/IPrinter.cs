using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.Printers
{
    public interface IPrinter
    {
        string GetTop100RowsQuery(DbObject obj);

        string GetDefinition(DefinitionObject obj);
    }
}