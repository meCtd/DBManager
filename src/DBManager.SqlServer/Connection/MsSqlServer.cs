using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Text;

namespace DataBaseTree.Model.DataBaseConnection
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
        public override DatabaseTypeEnum Type => DatabaseTypeEnum.MsSql;

        public override string DefaultDatabase => "master";

        public override string DefaultPort => "1433";

        public override string ConnectionString => GetConnectionString();

        private string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                Pooling = Pooling,
                DataSource = Server,
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

        public override DbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public override bool TestConnection()
        {
            bool conenctionSuccess = false;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    conenctionSuccess = true;
                }
                catch (Exception)
                {
                    conenctionSuccess = false;
                }
            }
            return conenctionSuccess;
        }

    }
}
