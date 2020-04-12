using System.Collections.Generic;
using System.Data.Common;

using System.Threading;
using System.Threading.Tasks;


namespace DBManager.Default.DataBaseConnection
{
    public interface IConnectionData
    {
        string Host { get; set; }

        string Port { get; set; }

        string InitialCatalog { get; set; }

        string UserId { get; set; }

        string Password { get; set; }

        IDictionary<ConnectionProperty, object> Properties { get; }

        DialectType Type { get; }

        string ConnectionString { get; }

        ServerInfo ServerInfo { get; }

        string TestQuery { get; }

        DbConnection GetConnection();

        Task<bool> TestConnectionAsync(CancellationToken token);
    }
}
