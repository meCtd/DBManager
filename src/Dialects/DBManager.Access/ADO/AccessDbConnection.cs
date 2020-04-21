using System;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using DBManager.Access.Connection;
using Microsoft.Office.Interop.Access.Dao;

namespace DBManager.Access.ADO
{
    class AccessDbConnection : DbConnection
    {
        private bool _isDisposed;
        private ConnectionState _state = ConnectionState.Closed;

        private static DBEngine _engine;
        private static DBEngine DBEngine => _engine ?? (_engine = new DBEngine());

        private Workspace _workspace;

        public Database DaoDatabase { get; private set; }
        internal string Password { get; }

        public override string ConnectionString { get; set; }
        public override string Database { get; }
        public override string DataSource { get; }
        public override string ServerVersion => DaoDatabase.Version;
        public override ConnectionState State => _state;

        public AccessDbConnection(AccessConnectionData connectionData)
        {
            ConnectionString = connectionData.ConnectionString;
            DataSource = connectionData.DataSource;
            Password = connectionData.Password;
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            Dispose();
        }

        public override void Open()
        {
            _workspace = DBEngine.CreateWorkspace(Guid.NewGuid().ToString(), "admin", string.Empty, WorkspaceTypeEnum.dbUseJet);
            DaoDatabase = _workspace.OpenDatabase(DataSource, false, false, $"MS Access; PWD ={Password}");

            _state = ConnectionState.Open;
            _isDisposed = false;
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            DaoDatabase.BeginTrans();
            return new AccessDbTransaction(isolationLevel, this);
        }

        protected override DbCommand CreateDbCommand()
        {
            return new AccessDbCommand(string.Empty, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (DaoDatabase != null)
                {
                    DaoDatabase.Close();
                    Marshal.ReleaseComObject(DaoDatabase);
                    DaoDatabase = null;
                }

                if (_workspace != null)
                {
                    _workspace.Close();
                    Marshal.ReleaseComObject(_workspace);
                    _workspace = null;
                }
            }

            _state = ConnectionState.Closed;
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
