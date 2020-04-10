using System.Collections.Generic;
using System.Data.Common;

using System.Threading;
using System.Threading.Tasks;


namespace DBManager.Default.DataBaseConnection
{
    public interface IConnectionData
    {
        string Host { get; }

        string Port { get; }

        string InitialCatalog { get; }

        string UserId { get; }

        string Password { get; }

        IReadOnlyDictionary<ConnectionProperty, object> Properties { get; }

        DialectType Type { get; }

        string ConnectionString { get; }

        ServerInfo ServerInfo { get; }

        string TestQuery { get; }

        DbConnection GetConnection();

        Task<bool> TestConnectionAsync(CancellationToken token);
    }
}
