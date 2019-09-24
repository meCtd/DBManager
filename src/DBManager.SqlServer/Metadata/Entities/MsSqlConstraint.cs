using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlConstraint : Constraint
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlConstraint(string name) : base(name)
		{
		}
	}
}
