using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;


namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    internal class SqlServerTableLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Table;

        public SqlServerTableLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
