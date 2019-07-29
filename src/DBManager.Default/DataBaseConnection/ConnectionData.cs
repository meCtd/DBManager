using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace DBManager.Default.DataBaseConnection
{
    [DataContract(Name = "ConnectionData")]
    public abstract class ConnectionData
    {
        public abstract DialectType Type { get; }

        [DataMember(Name = "Server")]
        public string Server { get; set; }

        [DataMember(Name = "Port")]
        public uint Port { get; set; }

        [DataMember(Name = "InitialCatalog")]
        public string InitialCatalog { get; set; }

        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        protected abstract string DefaultDatabase { get; }

        protected abstract string DefaultPort { get; }

        public string Password { get; set; }

        public abstract string ConnectionString { get; }

        public abstract DbConnection GetConnection();

        public Task<bool> TestConnectionAsync()
        {
            return TestConnectionAsync(CancellationToken.None);
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
