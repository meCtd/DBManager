using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerFunctionLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Function;

        public SqlServerFunctionLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
