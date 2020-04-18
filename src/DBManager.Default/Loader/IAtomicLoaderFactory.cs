using DBManager.Default.Tree;

namespace DBManager.Default.Loader
{
    public interface IAtomicLoaderFactory
    {
        IAtomicLoader GetAtomicLoader(MetadataType type);
    }
}