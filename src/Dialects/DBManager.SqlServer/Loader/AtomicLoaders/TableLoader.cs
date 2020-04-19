using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;


namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    internal class TableLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Table;

        public TableLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
