using System.Text;
using System.Threading.Tasks;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;
using DBManager.Default.Providers;
using DBManager.Default.Tree.DbEntities;
using DBManager.Default.Tree.Hierarchy;
using DBManager.SqlServer.Metadata;
using DBManager.SqlServer.Provider;

namespace DBManager.SqlServer.Loader
{
    public class SqlServerLoader : LoaderBase
    {
        public override IScriptProvider ScriptProvider { get; } = new SqlServerScriptProvider();
        public override IMetadataHierarchy Hierarchy { get; } = new SqlServerHierarchy();

        public SqlServerLoader(IDialectComponent components) 
            : base(new SqlServerAtomicLoaderFactory(components))
        {
        }

        public override async Task<Server> LoadServerAsync(ILoadingContext loadingContext)
        {
            return await Task.Run(() => new Server(loadingContext.ConnectionData, GetServerName(loadingContext.ConnectionData), DialectType.SqlServer));
        }

        private string GetServerName(IConnectionData data)
        {
            var builder = new StringBuilder();
            builder.Append(data.Host);

            if (!string.IsNullOrEmpty(data.Port))
                builder.Append($":{data.Port}");

            builder.Append($" ({data.UserId})");
            return builder.ToString();
        }
    }
}
