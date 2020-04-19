using System.Threading.Tasks;

using DBManager.Default.Providers;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;
using DBManager.Default.Tree.Hierarchy;


namespace DBManager.Default.Loader
{
    public interface ILoader
    {
        IScriptProvider ScriptProvider { get; }

        IMetadataHierarchy Hierarchy { get; }

        Task<Server> LoadServerAsync(ILoadingContext context);

        Task LoadChildrenAsync(ILoadingContext context, DbObject objectToLoad, MetadataType type = MetadataType.None);

        Task LoadPropertiesAsync(ILoadingContext context, DbObject objectToLoad);

        Task LoadDefinitionAsync(ILoadingContext context, DefinitionObject objectToLoad);
    }
}
