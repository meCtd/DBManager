using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerTableAtomicLoader : BaseAtomicLoaderViaSql
    {
        public override MetadataType Type => MetadataType.Table;

        public SqlServerTableAtomicLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
