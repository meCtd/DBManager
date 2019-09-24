using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlServer : Server
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlServer(string name) : base(name)
		{
		}
	}
}
