using System.Threading;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Default.Execution
{
    public interface IExecutionContext
    {
        IConnectionData Connection { get; }

        string Context { get; }

        CancellationToken Token { get; }
    }
}
