using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;


namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    internal class SchemaLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Schema;

        public SchemaLoader(IDialectComponent components)
            : base(components)
        { }
    }
}
