using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerViewLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.View;

        public SqlServerViewLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
