using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlTable : Table
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlTable(string name) : base(name)
		{
		}
	}
}
