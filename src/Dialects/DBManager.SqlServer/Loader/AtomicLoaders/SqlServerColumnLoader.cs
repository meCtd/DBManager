using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerColumnLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Column;

        public SqlServerColumnLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
