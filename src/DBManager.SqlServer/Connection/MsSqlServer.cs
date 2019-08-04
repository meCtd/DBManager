using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using Framework.Extensions;

namespace DBManager.SqlServer.Connection
{
    [DataContract(Name = "MsSqlServer")]
    public class MsSqlServer : ConnectionData
    {
        [DataMember(Name = "Type")]
        public override DialectType Type => DialectType.MsSql;

        protected override string DefaultDatabase => "master";

        public override string DefaultPort => "1433";

        public override string ConnectionString
        {
            get
            {
                bool integratedSecurity =
                    bool.Parse(Properties.GetValueOrDefault(ConnectionProperty.IntegratedSecurity, false.ToString()));

                var builder = new SqlConnectionStringBuilder
                {
                    Pooling = true,

                    DataSource = $"{Server},{(string.IsNullOrEmpty(Port) ? DefaultPort : Port)}",

                    IntegratedSecurity = integratedSecurity,

                    InitialCatalog = string.IsNullOrWhiteSpace(InitialCatalog) ? DefaultDatabase : InitialCatalog,

                    ApplicationName = nameof(DBManager)
                };

                if (integratedSecurity)
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
