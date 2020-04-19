using System;
using System.Threading.Tasks;

using DBManager.Access.Metadata;
using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Providers;
using DBManager.Default.Tree.DbEntities;
using DBManager.Default.Tree.Hierarchy;


namespace DBManager.Access.Loader
{
    internal class AccessLoader : LoaderBase
    {
        public override IScriptProvider ScriptProvider => throw new NotImplementedException();

        public override IMetadataHierarchy Hierarchy { get; } = new AccessHierarchy();

        public AccessLoader(IDialectComponent components)
            : base(new AccessAtomicLoaderFactory(components))
        {
        }

        public override async Task<Server> LoadServerAsync(ILoadingContext context)
        {
            return await Task.FromResult(new Server(context.ConnectionData, "Microsoft Access", DialectType.Access));
        }
    }
}
