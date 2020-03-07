using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

using DBManager.Default.DataBaseConnection;

namespace DBManager.Default.DataBaseConnection
{
    [DataContract(Name = "ConnectionData")]
    public abstract class ConnectionData
    {
        [DataMember(Name = "Server")]
        public string Server { get; set; }

        [DataMember(Name = "Port")]
        public string Port { get; set; }

        [DataMember(Name = "InitialCatalog")]
        public string InitialCatalog { get; set; }

        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        public string Password { get; set; }

        [DataMember(Name = "Properties")]
        public IDictionary<ConnectionProperty, object> Properties { get; }

        #region Abstract properties

        public abstract DialectType Type { get; }

        public abstract string ConnectionString { get; }

        public abstract string DefaultPort { get; }

        public abstract DbConnection GetConnection();

        protected abstract string DefaultDatabase { get; }

        #endregion

        public Task<bool> TestConnectionAsync()
        {
            return TestConnectionAsync(CancellationToken.None);
        }

        protected virtual IEnumerable<KeyValuePair<ConnectionProperty, object>> RegisterProperties()
        {
            return Enumerable.Empty<KeyValuePair<ConnectionProperty, object>>();
        }

        protected ConnectionData()
        {
            Properties = RegisterProperties().ToDictionary(s => s.Key, s => s.Value);
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
