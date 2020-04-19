using System.Threading;

using DBManager.Default.DataBaseConnection;


namespace DBManager.Default.Execution
{
    public class ExecutionContext : IExecutionContext
    {
        public IConnectionData Connection { get; }

        public string Context { get; }

        public CancellationToken Token { get; }

        public ExecutionContext(IConnectionData connection, string context, CancellationToken token)
        {
            Connection = connection;
            Context = context;
            Token = token;
        }
    }
}
