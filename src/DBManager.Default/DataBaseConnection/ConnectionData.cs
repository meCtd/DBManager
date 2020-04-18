using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace DBManager.Default.DataBaseConnection
{
    [DataContract(Name = "ConnectionData")]
    public abstract class ConnectionData : IConnectionData
    {
        [DataMember(Name = "server-info")]
        private ServerInfo _serverInfo = new ServerInfo();

        [DataMember(Name = "Host")]
        public string Host { get; set; }

        [DataMember(Name = "Port")]
        public string Port { get; set; }

        [DataMember(Name = "InitialCatalog")]
        public string InitialCatalog { get; set; }

        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        public string Password { get; set; }

        [DataMember(Name = "Properties")]
        public IDictionary<ConnectionProperty, object> Properties { get; }

        public abstract DialectType Dialect { get; }

        public abstract string ConnectionString { get; }

        public ServerInfo ServerInfo => _serverInfo;

        public abstract DbConnection GetConnection();

        public virtual string TestQuery => "SELECT 1";

        protected virtual IEnumerable<KeyValuePair<ConnectionProperty, object>> RegisterProperties()
        {
            return Enumerable.Empty<KeyValuePair<ConnectionProperty, object>>();
        }

        protected ConnectionData()
        {
            Properties = RegisterProperties()
                .ToDictionary(s => s.Key, s => s.Value);
        }

        public async Task<bool> TestConnectionAsync(CancellationToken token)
        {
            var result = true;
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync(token);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
