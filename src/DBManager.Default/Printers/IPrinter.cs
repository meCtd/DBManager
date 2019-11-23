using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.Printers
{
    public interface IPrinter
    {
        string GetDefinition(DefinitionObject obj);
    }
}