using System.Collections.Generic;

namespace DBManager.Default.Tree.Hierarchy
{
    public interface IMetadataHierarchy
    {
        DialectType Dialect { get; }
        IReadOnlyDictionary<MetadataType, MetadataHierarchyInfo> Structure { get; }
    }
}
