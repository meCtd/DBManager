using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlIndex : Index
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlIndex(string name) : base(name)
		{
		}
	}
}
