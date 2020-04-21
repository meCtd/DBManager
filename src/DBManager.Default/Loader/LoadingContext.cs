using System.Threading;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Default.Loader
{
    public class LoadingContext : ILoadingContext
    {
        public IConnectionData ConnectionData { get; }
        public CancellationToken Token { get; }

        public LoadingContext(IConnectionData connection, CancellationToken token)
        {
            ConnectionData = connection;
            Token = token;
        }
    }
}
