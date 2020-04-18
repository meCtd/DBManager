using DBManager.Default.Tree;

namespace DBManager.Default.Providers
{
    public interface IScriptProvider
    {
        string ProvideChangeContext();

        string ProvideNameScript(DbObject target, MetadataType childType);

        string ProvidePropertiesScript(DbObject obj);

        string ProvideDefinitionScript();

    }
}
