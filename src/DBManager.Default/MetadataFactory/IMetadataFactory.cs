using DBManager.Default.Tree;

namespace DBManager.Default.MetadataFactory
{
    public interface IMetadataFactory
    {
        DbObject Create(MetadataType type, string name);
    }
}
