﻿using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;

using Framework.Extensions;


namespace DBManager.SqlServer.Connection
{
    public class SqlServerConnectionData : ConnectionData
    {
        private const string DefaultDatabase = "master";

        public override DialectType Type => DialectType.SqlServer;

        public override string ConnectionString
        {
            get
            {
                bool integratedSecurity = (bool)Properties.GetValueOrDefault(ConnectionProperty.IntegratedSecurity, false);

                var port = string.IsNullOrEmpty(Port)
                    ? string.Empty
                    : $",{Port}";

                var builder = new SqlConnectionStringBuilder
                {
                    Pooling = true,

                    DataSource = $"{Host} {port}",

                    IntegratedSecurity = integratedSecurity,

                    UserID = UserId,

                    Password = Password,

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

        protected override IEnumerable<KeyValuePair<ConnectionProperty, object>> RegisterProperties()
        {
            yield return new KeyValuePair<ConnectionProperty, object>(ConnectionProperty.IntegratedSecurity, false);
        }

        public override DbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
