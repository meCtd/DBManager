using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    internal class SqlServerDatabaseLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Database;

        public SqlServerDatabaseLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
