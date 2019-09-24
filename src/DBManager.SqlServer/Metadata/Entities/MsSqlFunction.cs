using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlFunction : Function
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlFunction(string name) : base(name)
		{
		}
	}
}
