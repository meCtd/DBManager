using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using DBManager.Default.DataBaseConnection;
using DBManager.Default.Execution;

using DBManager.SqlServer.Connection;


namespace DBManager.SqlServer
{
    class SqlServerCreator : IComponentCreator
    {
        public DbCommand CreateCommand() => new SqlCommand();

        public IDataParameter CreateParameter() => new SqlParameter();

        public IConnectionData CreateConnectionData() => new SqlServerConnectionData();
    }
}
