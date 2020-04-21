using System.Collections.Generic;
using DBManager.Default;
using DBManager.Default.Tree;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.Access.Metadata
{
    class AccessHierarchy : IMetadataHierarchy
    {
        public DialectType Dialect => DialectType.Access;

        public MetadataType TopLevelObjectType => MetadataType.Database;

        private static readonly Dictionary<MetadataType, MetadataHierarchyInfo> _structure = new Dictionary<MetadataType, MetadataHierarchyInfo>
        {
            [MetadataType.Database] = new MetadataHierarchyInfo(MetadataType.Schema, new[] { MetadataType.Table,
                                                                                    MetadataType.View,
                                                                                    MetadataType.Procedure }),

            [MetadataType.Table] = new MetadataHierarchyInfo(MetadataType.Table, new[] { MetadataType.Column,
                                                                                    MetadataType.Constraint,
                                                                                    MetadataType.Index}),

            [MetadataType.View] = new MetadataHierarchyInfo(MetadataType.View, new[] { MetadataType.Column }),

            [MetadataType.Procedure] = new MetadataHierarchyInfo(MetadataType.Procedure, new[] { MetadataType.Parameter })
        };

        public IReadOnlyDictionary<MetadataType, MetadataHierarchyInfo> Structure => _structure;
    }
}
