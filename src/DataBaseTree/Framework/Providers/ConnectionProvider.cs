using System;
using System.Collections.Generic;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.SqlServer.Connection;

namespace DBManager.Application.Framework.Providers
{
    public class ConnectionProvider
    {
        private static ConnectionProvider _instance;

        public static ConnectionProvider Instance => _instance ?? (_instance = new ConnectionProvider());

        private ConnectionProvider()
        {
        }

        private static readonly Dictionary<DialectType, ConnectionData> _connectionCreator = new Dictionary<DialectType, ConnectionData>
        {
            [DialectType.MsSql] = new MsSqlServer()
        };


        public ConnectionData CreateConnectionData(DialectType dialect)
        {
            if (_connectionCreator.TryGetValue(dialect, out var result))
            {
                return result;
            }

            throw new NotSupportedException("Dialect is not supported yet!");
        }
    }
}
