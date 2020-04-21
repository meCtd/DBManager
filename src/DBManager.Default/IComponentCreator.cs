using System.Data;
using System.Data.Common;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Default
{
    public interface IComponentCreator
    {
        DbCommand CreateCommand();

        IDataParameter CreateParameter();

        IConnectionData CreateConnectionData();
    }
}
