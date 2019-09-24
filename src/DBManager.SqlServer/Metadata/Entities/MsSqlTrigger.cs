using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlTrigger : Trigger
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlTrigger(string name) : base(name)
		{
		}
	}
}
