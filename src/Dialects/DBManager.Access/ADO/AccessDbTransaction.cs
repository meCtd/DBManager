using System.Data;
using System.Data.Common;

namespace DBManager.Access.ADO
{
    class AccessDbTransaction : DbTransaction
    {
        private IsolationLevel _isolationLevel;
        private DbConnection _connection;

        public override IsolationLevel IsolationLevel => _isolationLevel;
        protected override DbConnection DbConnection => _connection;

        public AccessDbTransaction(IsolationLevel isolationLevel, DbConnection connection)
        {
            _connection = connection;
            _isolationLevel = IsolationLevel;
        }

        public override void Commit()
        {
            ((AccessDbConnection)DbConnection).DaoDatabase.CommitTrans();
        }

        public override void Rollback()
        {
            ((AccessDbConnection)DbConnection).DaoDatabase.Rollback();
        }
    }
}
