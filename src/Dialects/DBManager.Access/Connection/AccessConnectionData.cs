using System.Data.Common;
using DBManager.Access.ADO;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Access.Connection
{
    class AccessConnectionData : ConnectionData
    {
        public override DialectType Dialect => DialectType.Access;

        public string DataSource 
        { 
            get => Host; 
            set => Host = value; 
        }

        public override string ConnectionString
        {
            get
            {
                var csb = new AccessConnectionStringBuilder
                {
                    DataSource = Host,
                    Password = Password
                };

                return csb.ToString();
            }
        }

        public override DbConnection GetConnection() => new AccessDbConnection(this);
    }
}
