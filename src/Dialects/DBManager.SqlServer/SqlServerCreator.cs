using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.SqlServer.Connection;


namespace DBManager.SqlServer
{
    internal class SqlServerCreator : IComponentCreator
    {
        internal static IComponentCreator Instance { get; } = new SqlServerCreator();

        private SqlServerCreator()
        {
        }

        public DbCommand CreateCommand() => new SqlCommand();

        public IDataParameter CreateParameter() => new SqlParameter();

        public IConnectionData CreateConnectionData() => new SqlServerConnectionData();
    }
}
