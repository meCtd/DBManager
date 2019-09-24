using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlKey : Key
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlKey(string name) : base(name)
		{
		}
	}
}
