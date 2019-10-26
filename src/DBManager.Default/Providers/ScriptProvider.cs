using System.Collections.Generic;
using System.Data;
using DBManager.Default.Tree;

namespace DBManager.Default.Providers
{
    public interface IScriptProvider
    {
        string ProvideNameScript(MetadataType parentType, MetadataType childType);

        string ProvidePropertiesScript(DbObject obj);

        string ProvideDefinitionScript();

        IEnumerable<IDbDataParameter> GetDefinitionParameters(DbObject obj);

        IEnumerable<IDbDataParameter> GetChildrenLoadParameters(DbObject obj, MetadataType childType);

        IEnumerable<IDbDataParameter> GetLoadPropertiesParameters(DbObject obj);

    }
}
