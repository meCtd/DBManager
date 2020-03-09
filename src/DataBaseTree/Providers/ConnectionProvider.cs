using System;
using System.Collections.Generic;
using DBManager.Application.Providers.Abstract;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.SqlServer.Connection;

namespace DBManager.Application.Providers
{
    public class ConnectionProvider : IConnectionProvider
    {
        private static readonly Dictionary<DialectType, Func<ConnectionData>> _connectionCreator = new Dictionary<DialectType, Func<ConnectionData>>
        {
            [DialectType.MsSql] = () => new MsSqlServer()
        };


        public ConnectionData ProvideConnection(DialectType dialect)
        {
            if (_connectionCreator.TryGetValue(dialect, out var result))
            {
                return result();
            }

            throw new NotSupportedException("Dialect is not supported yet!");
        }
    }
}
