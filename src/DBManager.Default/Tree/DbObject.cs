using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DBManager.Default.Tree
{
	[DataContract(Name = "db-object", IsReference = true)]
	[KnownType(typeof(DBNull))]
	public abstract class DbObject
	{
		#region Fields

		[DataMember(Name = "Children")]
		private List<KeyValuePair<MetadataType, List<DbObject>>> _childrenList;

		[DataMember(Name = "Properties")]
		private List<KeyValuePair<string, object>> _propertyList;


		private Dictionary<MetadataType, List<DbObject>> _childrenMap;

		private FullName _fullName;

		private string _databaseName;

		private string _schemaName;

		#endregion

		#region Properties

		public abstract MetadataType Type { get; }

		public abstract DialectType Dialect { get; }

		public abstract bool CanHaveDefinition { get; }

		public string Definition { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; private set; }

		[DataMember(Name = "is-property-loaded")]
		public bool IsPropertyLoaded { get; set; }

		public FullName FullName => _fullName ?? (_fullName = LoadName());

		[DataMember(Name = "parent")]
		public DbObject Parent { get; private set; }

		public string SchemaName => _schemaName ?? (_schemaName = GetBaseName(MetadataType.Schema));

		public string DataBaseName => _databaseName ?? (_databaseName = GetBaseName(MetadataType.Database));

		public virtual IReadOnlyList<DbObject> Children => _childrenMap.Values.SelectMany(x => x).ToList();

		public Dictionary<string, object> Properties { get; private set; }

		#endregion

		protected DbObject(string name)
		{
			Name = name;
			Properties = new Dictionary<string, object>();
			_childrenMap = new Dictionary<MetadataType, List<DbObject>>();
		}

		#region Methods

		#region Private

		private void SetParent(DbObject dbObject)
		{
			dbObject.Parent = this;
		}

		private FullName LoadName()
		{
			FullName fullName = new FullName(this);
			DbObject parent = Parent;
			while (parent != null && parent.Type != MetadataType.Server)
			{
				fullName.AddParent(parent);
				parent = parent.Parent;
			}

			return fullName;
		}

		private string GetBaseName(MetadataType type)
		{
			if (Type == type)
				return this.Name;

			foreach (var chunc in FullName)
			{
				if (chunc.Type == type)
				{
					return chunc.Name;
				}
			}
			return null;
		}

		#endregion

		#region Public

		#region Actions with childs

		public void DeleteProperties()
		{
			Properties.Clear();
		}

		public void DeleteChildrens()
		{
			_childrenMap.Clear();
		}

		public void DeleteChildrens(MetadataType type)
		{
			if (_childrenMap.ContainsKey(type))
				_childrenMap.Remove(type);
		}

		public virtual bool AddChild(DbObject obj)
		{
			List<DbObject> items = _childrenMap.ContainsKey(obj.Type) ? _childrenMap[obj.Type] : (_childrenMap[obj.Type] = new List<DbObject>());
			items.Add(obj);
			SetParent(obj);

			return true;
		}

		public virtual bool RemoveChild(DbObject obj)
		{
			bool result = _childrenMap[obj.Type].Remove(obj);

			if (result)
			{
				obj.Parent = null;

			}
			return result;
		}

		public virtual bool ReplaceChild(DbObject oldChild, DbObject newChild)
		{
			if (!CanBeChild(oldChild) || !CanBeChild(newChild))
				return false;

			if (oldChild.Type != newChild.Type)
				return false;

			int index = _childrenMap[oldChild.Type].FindIndex((o) => o.Equals(oldChild));

			if (index == -1)
				return false;

			_childrenMap[oldChild.Type][index] = newChild;

			oldChild.Parent = null;
			SetParent(newChild);

			return true;
		}

		public bool? IsChildrenLoaded(MetadataType? childType)
		{
			if (!HierarhyOld.HierarchyObject.IsPossibleChilds(Type))
				return true;

			if (childType == null)
			{
				if (_childrenMap.Count == 0)
					return false;

				if (HierarhyOld.HierarchyObject.GetChildTypes(Type).Any(type => !_childrenMap.ContainsKey(type)))
				{
					return null;
				}

				return true;
			}
			else
				return _childrenMap.ContainsKey(childType.Value);
		}

		#endregion

		public void UpdateFullName()
		{
			_fullName = LoadName();
		}

		public override string ToString()
		{
			return Name;
		}

		[OnSerializing]
		public void Save(StreamingContext context)
		{
			_childrenList = _childrenMap.ToList();
			_propertyList = Properties.ToList();
		}

		[OnDeserialized]
		public void Update(StreamingContext context)
		{
			_childrenMap = new Dictionary<MetadataType, List<DbObject>>();
			Properties = new Dictionary<string, object>();

			if (_childrenList != null)
				foreach (var childType in _childrenList)
				{
					_childrenMap[childType.Key] = childType.Value;

				}

			if (_propertyList != null)

				foreach (var property in _propertyList)
				{
					Properties[property.Key] = property.Value;
				}
		}

		#endregion

		#endregion
	}
}
