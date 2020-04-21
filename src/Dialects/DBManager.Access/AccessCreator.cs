﻿using System;
using System.Data;
using System.Data.Common;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;


namespace DBManager.Access
{
    internal class AccessCreator : IComponentCreator
    {
        public DbCommand CreateCommand() => new AccessDbCommand();

        public IDataParameter CreateParameter() => throw new NotImplementedException();

        public IConnectionData CreateConnectionData() => new AccessConnectionData();
    }
}