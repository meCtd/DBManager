using System.Threading.Tasks;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.Loader
{
    public interface IAtomicLoader
    {
        MetadataType Type { get; }

        Task LoadChildren(ILoadingContext context, DbObject objectToLoad);
        Task LoadProperties(ILoadingContext context, DbObject objectToLoad);
        Task LoadDefinition(ILoadingContext context, DefinitionObject objectToLoad);
    }
}