using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerTriggerLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Trigger;

        public SqlServerTriggerLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
