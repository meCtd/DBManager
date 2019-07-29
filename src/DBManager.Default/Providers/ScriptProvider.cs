using System.Collections.Generic;
using System.Data;
using DBManager.Default.Tree;

namespace DBManager.Default.Providers
{
	public abstract class ScriptProvider
	{
		public abstract string GetLoadNameScript(DbEntityType parentType,DbEntityType childType);

		public abstract string GetPropertiesScript(DbObject obj);

		public abstract string GetDefinitionScript();

		public abstract IEnumerable<IDbDataParameter> GetDefinitionParameters(DbObject obj);

		public abstract IEnumerable<IDbDataParameter> GetChildrenLoadParameters(DbObject obj, DbEntityType childType);

		public abstract IEnumerable<IDbDataParameter> GetLoadPropertiesParameters(DbObject obj);

	}
}
