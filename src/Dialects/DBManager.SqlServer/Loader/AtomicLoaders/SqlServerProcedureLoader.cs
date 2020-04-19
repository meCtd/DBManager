using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerProcedureLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Procedure;

        public SqlServerProcedureLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
