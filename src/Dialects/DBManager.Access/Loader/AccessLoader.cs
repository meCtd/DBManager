using System;
using System.IO;
using System.Threading.Tasks;
using DBManager.Access.Connection;
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
            var connection = (AccessConnectionData)context.ConnectionData;
            var name = Path.GetFileNameWithoutExtension(connection.DataSource);

            return await Task.FromResult(new Server(context.ConnectionData, name, DialectType.Access));
        }
    }
}
