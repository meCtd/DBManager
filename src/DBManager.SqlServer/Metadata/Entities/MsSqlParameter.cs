using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlParameter : Parameter
	{
		public override DialectType Dialect => DialectType.MsSql;

		public MsSqlParameter(string name, DbType parameterType) : base(name, parameterType)
		{
		}
	}
}
