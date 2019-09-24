using System.Collections.Generic;
using System.Data;
using DBManager.Default.Tree;

namespace DBManager.Default.Providers
{
	public abstract class ScriptProvider
	{
		public abstract string GetLoadNameScript(MetadataType parentType,MetadataType childType);

		public abstract string GetPropertiesScript(DbObject obj);

		public abstract string GetDefinitionScript();

		public abstract IEnumerable<IDbDataParameter> GetDefinitionParameters(DbObject obj);

		public abstract IEnumerable<IDbDataParameter> GetChildrenLoadParameters(DbObject obj, MetadataType childType);

		public abstract IEnumerable<IDbDataParameter> GetLoadPropertiesParameters(DbObject obj);

	}
}
