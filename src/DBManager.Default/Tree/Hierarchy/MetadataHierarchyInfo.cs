using System.Collections.Generic;

namespace DBManager.Default.Tree.Hierarchy
{
	public class MetadataHierarchyInfo
	{
		public MetadataHierarchyInfo(MetadataType currentType, IEnumerable<MetadataType> childrenTypes, bool needCategory)
		{
			CurrentType = currentType;
			ChildrenTypes = childrenTypes;
			NeedCategory = needCategory;
		}

		public MetadataType CurrentType { get; }
		public IEnumerable<MetadataType> ChildrenTypes { get; }
		public bool NeedCategory { get; }
	}
}
