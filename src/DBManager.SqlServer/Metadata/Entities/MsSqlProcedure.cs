using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlProcedure : Procedure
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlProcedure(string name) : base(name)
		{
		}
	}
}
