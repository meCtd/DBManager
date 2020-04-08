using System.Collections.Generic;
using DBManager.Default;
using DBManager.Default.Tree;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.SqlServer.Metadata
{
    internal class SqlServerHierarchy : IMetadataHierarchy
    {
        public DialectType Dialect => DialectType.MsSql;

        public MetadataType TopLevelObjectType => MetadataType.Database;

        private static readonly Dictionary<MetadataType, MetadataHierarchyInfo> _structure = new Dictionary<MetadataType, MetadataHierarchyInfo>
        {
            [MetadataType.Server] = new MetadataHierarchyInfo(MetadataType.Server, new[] { MetadataType.Database }),

            [MetadataType.Database] = new MetadataHierarchyInfo(MetadataType.Database, new[] { MetadataType.Schema }),

            [MetadataType.Schema] = new MetadataHierarchyInfo(MetadataType.Schema, new[] { MetadataType.Table,
                                                                                    MetadataType.View,
                                                                                    MetadataType.Procedure,
                                                                                    MetadataType.Function }),

            [MetadataType.Table] = new MetadataHierarchyInfo(MetadataType.Table, new[] { MetadataType.Column,
                                                                                    MetadataType.Key,
                                                                                    MetadataType.Constraint,
                                                                                    MetadataType.Trigger,
                                                                                    MetadataType.Index}),

            [MetadataType.Table] = new MetadataHierarchyInfo(MetadataType.Table, new[] { MetadataType.Column,
                                                                                    MetadataType.Key,
                                                                                    MetadataType.Constraint,
                                                                                    MetadataType.Trigger,
                                                                                    MetadataType.Index }),

            [MetadataType.View] = new MetadataHierarchyInfo(MetadataType.View, new[] { MetadataType.Column }),

            [MetadataType.Procedure] = new MetadataHierarchyInfo(MetadataType.Procedure, new[] { MetadataType.Parameter }),
            [MetadataType.Function] = new MetadataHierarchyInfo(MetadataType.Function, new[] { MetadataType.Parameter }),
        };

        public IReadOnlyDictionary<MetadataType, MetadataHierarchyInfo> Structure => _structure;
    }
}
