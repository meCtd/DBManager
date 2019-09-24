using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlView : View
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlView(string name) : base(name)
		{
		}
	}
}
