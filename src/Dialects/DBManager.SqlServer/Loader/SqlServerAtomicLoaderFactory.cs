using System.Collections.Generic;
using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.SqlServer.Loader.AtomicLoaders;

namespace DBManager.SqlServer.Loader
{
    internal class SqlServerAtomicLoaderFactory : AtomicLoaderFactoryBase
    {
        protected override Dictionary<MetadataType, IAtomicLoader> AtomicLoaders { get; }

        public SqlServerAtomicLoaderFactory(IDialectComponent components)
            : base(components)
        {
            AtomicLoaders = new Dictionary<MetadataType, IAtomicLoader>()
            {
                [MetadataType.Database] = new SqlServerDatabaseAtomicLoader(components),
                [MetadataType.Schema] = new SqlServerSchemaAtomicLoader(components),
                [MetadataType.Table] = new SqlServerTableAtomicLoader(components),
            };
        }
    }
}