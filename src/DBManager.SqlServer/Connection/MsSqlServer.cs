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
                    Pooling = true,
                    DataSource = $"{Server},{(string.IsNullOrEmpty(Port) ? DefaultPort : Port)}",
                    IntegratedSecurity = IntegratedSecurity,
                    InitialCatalog = string.IsNullOrWhiteSpace(InitialCatalog) ? DefaultDatabase : InitialCatalog,
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
