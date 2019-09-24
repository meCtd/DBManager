using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlDatabase : Database
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlDatabase(string name) : base(name)
		{
		}
	}
}
