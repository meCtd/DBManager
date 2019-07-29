using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;

namespace DBManager.SqlServer.Connection
{
    [DataContract(Name = "MsSqlServer")]
    public class MsSqlServer : ConnectionData
    {
        [DataMember(Name = "IntegratedSecurity")]
        public bool IntegratedSecurity { get; set; }

        [DataMember(Name = "Pooling")]
        public bool Pooling { get; set; }

        [DataMember(Name = "ConnectionTimeout")]
        public int ConnectionTimeout { get; set; }

        [DataMember(Name = "Type")]
        public override DialectType Type => DialectType.MsSql;

        protected override string DefaultDatabase => "master";

        protected override string DefaultPort => "1433";

        public override string ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    Pooling = Pooling,
                    DataSource = $"{Server},{Port.ToString()}",
                    IntegratedSecurity = IntegratedSecurity,
                    InitialCatalog = string.IsNullOrWhiteSpace(InitialCatalog) ? DefaultDatabase : InitialCatalog,
                    ConnectTimeout = ConnectionTimeout
                };

                if (IntegratedSecurity)
                {
                    builder.UserID = UserId;
                    builder.Password = Password;
                }

                return builder.ToString();
            }
        }
        
        public override DbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
