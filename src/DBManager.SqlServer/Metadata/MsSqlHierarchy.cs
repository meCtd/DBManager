using System.Collections.Generic;
using DBManager.Default;
using DBManager.Default.Tree;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlHierarchy : IMetadataHierarchy
	{
		#region Singleton
		private static readonly MsSqlHierarchy _instance;
		public static MsSqlHierarchy Instance = _instance ?? (_instance = new MsSqlHierarchy());

		private MsSqlHierarchy() { }
		#endregion

		public DialectType Dialect => DialectType.MsSql;

		private Dictionary<MetadataType, MetadataHierarchyInfo> _structure = new Dictionary<MetadataType, MetadataHierarchyInfo>
		{
			//[MetadataType.Server] = new MetadataHierarchyInfo(MetadataType.Server, new[] { MetadataType.Database }, false),

			[MetadataType.Database] = new MetadataHierarchyInfo(MetadataType.Database, new[] { MetadataType.Schema }, true),

			[MetadataType.Schema] = new MetadataHierarchyInfo(MetadataType.Schema, new[] { MetadataType.Table,
																					MetadataType.View,
																					MetadataType.Procedure,
																					MetadataType.Function }, true),

			[MetadataType.Table] = new MetadataHierarchyInfo(MetadataType.Table, new[] { MetadataType.Column,
																					MetadataType.Key,
																					MetadataType.Constraint,
																					MetadataType.Trigger,
																					MetadataType.Index}, true),

			[MetadataType.Table] = new MetadataHierarchyInfo(MetadataType.Table, new[] { MetadataType.Column,
																					MetadataType.Key,
																					MetadataType.Constraint,
																					MetadataType.Trigger,
																					MetadataType.Index }, true),

			[MetadataType.View] = new MetadataHierarchyInfo(MetadataType.View, new[] { MetadataType.Column }, true),

			[MetadataType.Procedure] = new MetadataHierarchyInfo(MetadataType.Procedure, new[] { MetadataType.Parameter }, true),
			[MetadataType.Function] = new MetadataHierarchyInfo(MetadataType.Function, new[] { MetadataType.Parameter }, true),
		};

		public IReadOnlyDictionary<MetadataType, MetadataHierarchyInfo> Structure => _structure;
	}
}
