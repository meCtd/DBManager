using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerSchemaAtomicLoader : BaseAtomicLoaderViaSql
    {
        public override MetadataType Type => MetadataType.Schema;

        public SqlServerSchemaAtomicLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
