using System.Collections.Generic;
using System.Linq;

namespace DataBaseTree.Model.Tree
{
	public class Hierarchy
	{
		private static Hierarchy _source;

		private Dictionary<DbEntityType, IEnumerable<DbEntityType>> _childrenTypes;

		public static IEnumerable<DbEntityType> Empty = new DbEntityType[0];

		public IReadOnlyDictionary<DbEntityType, IEnumerable<DbEntityType>> ChildresTypes =>
			_childrenTypes ?? (_childrenTypes = SetChildren());

		public static Hierarchy HierarchyObject
		{
			get { return _source ?? (_source = new Hierarchy()); }
		}

		private Hierarchy()
		{

		}

		protected virtual Dictionary<DbEntityType, IEnumerable<DbEntityType>> SetChildren()
		{
			Dictionary<DbEntityType, IEnumerable<DbEntityType>> dictionary =
				new Dictionary<DbEntityType, IEnumerable<DbEntityType>>
				{
					[DbEntityType.Server] = new List<DbEntityType>() { DbEntityType.Database },
					[DbEntityType.Database] = new List<DbEntityType>()
					{
						DbEntityType.Schema
					},
					[DbEntityType.Schema] = new List<DbEntityType>()
					{
						DbEntityType.Table,
						DbEntityType.View,
						DbEntityType.Procedure,
						DbEntityType.Function
					},
					[DbEntityType.Table] = new List<DbEntityType>()
					{
						DbEntityType.Column,
						DbEntityType.Key,
						DbEntityType.Constraint,
						DbEntityType.Trigger,
						DbEntityType.Index
					},
					[DbEntityType.View] =
						new List<DbEntityType>() { DbEntityType.Column, DbEntityType.Trigger, DbEntityType.Index },
					[DbEntityType.Procedure] = new List<DbEntityType>() { DbEntityType.Parameter },
					[DbEntityType.Function] = new List<DbEntityType>() { DbEntityType.Parameter }
				};
			return dictionary;
		}

		public IEnumerable<DbEntityType> GetChildTypes(DbEntityType type)
		{
			return ChildresTypes.ContainsKey(type) ? ChildresTypes[type] : Empty;
		}

		public bool IsPossibleChilds(DbEntityType type)
		{
			return GetChildTypes(type).Count() != 0;
		}
	}
}
