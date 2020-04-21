using System.Threading;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Default.Loader
{
    public interface ILoadingContext
    {
        IConnectionData ConnectionData { get; }
        CancellationToken Token { get; }
    }
}
