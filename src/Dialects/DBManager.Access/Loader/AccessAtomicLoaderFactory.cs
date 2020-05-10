using System.Collections.Generic;

using DBManager.Access.Loader.AtomicLoaders;
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
            AtomicLoaders = new Dictionary<MetadataType, IAtomicLoader>()
            {
                [MetadataType.Table] = new AccessTableLoader(),
                [MetadataType.Column] = new AccessColumnLoader(),
                [MetadataType.View] = new AccessViewLoader(),
                [MetadataType.Procedure] = new AccessProcedureLoader(),
                [MetadataType.Parameter] = new AccessParameterLoader(),
                [MetadataType.Index] = new AccessIndexLoader(),
                [MetadataType.Constraint] = new AccessConstraintLoader(),
            };
        }
    }
}
