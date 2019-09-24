using System.Collections.Generic;

namespace DBManager.Default.Tree.Hierarchy
{
	public abstract class MetadataHierarchy
	{
		public abstract DialectType Dialect { get; }
		public abstract IReadOnlyDictionary<MetadataType, MetadataHierarchyInfo> HierarchyStructure { get; }
	}
}
