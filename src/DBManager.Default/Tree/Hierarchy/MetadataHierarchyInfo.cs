using System.Collections.Generic;

namespace DBManager.Default.Tree.Hierarchy
{
    public class MetadataHierarchyInfo
    {
        private readonly ICollection<MetadataType> _childrenTypes;

        public MetadataType Type { get; }

        public IEnumerable<MetadataType> ChildrenTypes => _childrenTypes;

        public bool HasChildren => _childrenTypes.Count > 0;

        public bool NeedCategory => _childrenTypes.Count > 1;

        public MetadataHierarchyInfo(MetadataType type, ICollection<MetadataType> childrenTypes)
        {
            Type = type;
            _childrenTypes = childrenTypes;
        }

    }
}
