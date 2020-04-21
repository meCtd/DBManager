using System;
using System.Data;
using System.Data.Common;

namespace DBManager.Access.ADO
{
	class AccessDbCommand : DbCommand
	{
		public override string CommandText { get; set; }
		public override int CommandTimeout { get; set; }
		public override CommandType CommandType { get; set; }
		public override bool DesignTimeVisible { get; set; }
		public override UpdateRowSource UpdatedRowSource { get; set; }
		protected override DbConnection DbConnection { get; set; }
		protected override DbParameterCollection DbParameterCollection { get; }
		protected override DbTransaction DbTransaction { get; set; }

		public AccessDbCommand()
        { }

		public AccessDbCommand(string cmdText, AccessDbConnection connection) : this()
		{
			CommandText = cmdText;
			DbConnection = connection;
		}

		public override int ExecuteNonQuery()
		{
			var connection = (AccessDbConnection)DbConnection;

			return AccessDbDataReader.CreateDbReader(connection, CommandText).RecordsAffected;
		}

		public override object ExecuteScalar()
		{
			var connection = (AccessDbConnection)DbConnection;

			connection.DaoDatabase.Execute(CommandText);
			return connection.DaoDatabase.OpenRecordset(CommandText).Fields[0].Value;
		}

		public override void Cancel()
		{
			throw new NotSupportedException();
		}

		public override void Prepare()
		{
			throw new NotSupportedException();
		}

		protected override DbParameter CreateDbParameter()
		{
			throw new NotSupportedException();
		}

		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			var connection = (AccessDbConnection)DbConnection;

			return AccessDbDataReader.CreateDbReader(connection, CommandText);
		}
	}
}
