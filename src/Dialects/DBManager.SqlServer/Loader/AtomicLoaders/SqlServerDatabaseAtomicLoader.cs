using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerDatabaseAtomicLoader : BaseAtomicLoaderViaSql
    {
        public override MetadataType Type => MetadataType.Database;

        public SqlServerDatabaseAtomicLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
