using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlColumn : Column
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlColumn(string name, DbType columnType) : base(name, columnType)
		{
		}
	}
}
