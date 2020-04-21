using System.Collections.Generic;
using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;

namespace DBManager.Access.Loader
{
    class AccessAtomicLoaderFactory : AtomicLoaderFactoryBase
    {
        protected override Dictionary<MetadataType, IAtomicLoader> AtomicLoaders { get; }

        public AccessAtomicLoaderFactory(IDialectComponent components)
            : base(components)
        {
            AtomicLoaders = new Dictionary<MetadataType, IAtomicLoader>();
        }
    }
}
