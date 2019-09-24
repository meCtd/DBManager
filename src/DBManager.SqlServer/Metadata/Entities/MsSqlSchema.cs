using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlSchema : Schema
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlSchema(string name) : base(name)
		{
		}
	}
}
