using System.Threading.Tasks;

using DBManager.Default.Providers;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;
using DBManager.Default.Tree.Hierarchy;


namespace DBManager.Default.Loader
{
    public abstract class LoaderBase : ILoader
    {
        private readonly IAtomicLoaderFactory _atomicLoaderFactory;

        public abstract IScriptProvider ScriptProvider { get; }
        public abstract IMetadataHierarchy Hierarchy { get; }

        protected LoaderBase(IAtomicLoaderFactory loaderFactory)
        {
            _atomicLoaderFactory = loaderFactory;
        }

        public abstract Task<Server> LoadServerAsync(ILoadingContext context);
        
        public async Task LoadChildrenAsync(ILoadingContext context, DbObject objectToLoad, MetadataType type = MetadataType.None)
        {
            if (!Hierarchy.Structure[objectToLoad.Type].HasChildren)
                return;

            if (type != MetadataType.None)
                await LoadChildrenByTypeAsync(context, objectToLoad, type);
            else
            {
                foreach (var metadataType in Hierarchy.Structure[objectToLoad.Type].ChildrenTypes)
                {
                    await LoadChildrenByTypeAsync(context, objectToLoad, metadataType);
                }
            }
        }

        public async Task LoadDefinitionAsync(ILoadingContext context, DefinitionObject objectToLoad)
        {
            var atomicLoader = _atomicLoaderFactory.GetAtomicLoader(objectToLoad.Type);

            await atomicLoader.LoadDefinition(context, objectToLoad);
        }

        public async Task LoadPropertiesAsync(ILoadingContext context, DbObject objectToLoad)
        {
            var atomicLoader = _atomicLoaderFactory.GetAtomicLoader(objectToLoad.Type);

            await atomicLoader.LoadProperties(context, objectToLoad);
        }

        private async Task LoadChildrenByTypeAsync(ILoadingContext context, DbObject objectToLoad, MetadataType type)
        {
            await _atomicLoaderFactory
                .GetAtomicLoader(type)
                .LoadChildren(context, objectToLoad);
        }
    }
}
