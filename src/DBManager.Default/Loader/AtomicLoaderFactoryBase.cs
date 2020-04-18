using System;
using System.Collections.Generic;

using DBManager.Default.Tree;

namespace DBManager.Default.Loader
{
    public abstract class AtomicLoaderFactoryBase : IAtomicLoaderFactory
    {
        protected readonly IDialectComponent _components;

        protected abstract Dictionary<MetadataType, IAtomicLoader> AtomicLoaders { get; }

        public AtomicLoaderFactoryBase(IDialectComponent components)
        {
            _components = components;
        }

        public IAtomicLoader GetAtomicLoader(MetadataType type)
        {
            if (AtomicLoaders.TryGetValue(type, out IAtomicLoader atomicLoader))
                return atomicLoader;

            throw new NotImplementedException();
        }
    }
}
