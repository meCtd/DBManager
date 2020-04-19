using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;


namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    internal class SqlServerSchemaLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Schema;

        public SqlServerSchemaLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
