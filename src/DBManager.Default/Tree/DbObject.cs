using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.Default.Tree
{
    [DataContract(Name = "db-object", IsReference = true)]
    [KnownType(typeof(DBNull))]
    public abstract class DbObject
    {
        #region Fields

        [DataMember(Name = "Children")]
        private List<KeyValuePair<MetadataType, List<DbObject>>> _children;

        [DataMember(Name = "Properties")]
        private List<KeyValuePair<string, object>> _properties;

        private Dictionary<MetadataType, List<DbObject>> _childrenMap;

        private FullName _fullName;

        private string _databaseName;

        private string _schemaName;

        #endregion

        #region Properties

        public abstract MetadataType Type { get; }

        public abstract bool CanHaveDefinition { get; }

        public string Definition { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "is-property-loaded")]
        public bool IsPropertyLoaded { get; set; }

        public FullName FullName => _fullName ?? (_fullName = GetName());

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

        private FullName GetName()
        {
            FullName fullName = new FullName(this);
            DbObject parent = Parent;
            while (parent != null)
            {
                fullName.AddParent(parent);
                parent = parent.Parent;
            }

            return fullName;
        }

        private string GetBaseName(MetadataType type)
        {
            if (Type == type)
                return Name;

            foreach (var chunk in FullName)
            {
                if (chunk.Type == type)
                {
                    return chunk.Name;
                }
            }

            return string.Empty;
        }

        #endregion

        #region Public

        #region Actions with childs

        public void DeleteProperties()
        {
            Properties.Clear();
        }

        public void DeleteChildren()
        {
            _childrenMap.Clear();
        }

        public void DeleteChildren(MetadataType type)
        {
            if (_childrenMap.ContainsKey(type))
                _childrenMap.Remove(type);
        }

        public virtual bool AddChild(DbObject obj)
        {
            var items = _childrenMap.ContainsKey(obj.Type) ? _childrenMap[obj.Type] : (_childrenMap[obj.Type] = new List<DbObject>());
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

        //REMOVE METHOD IF NOT USED
        public virtual bool ReplaceChild(DbObject oldChild, DbObject newChild, IMetadataHierarchy hierarchy)
        {
            //TODO: CHANGE FROM HIERARCHY
            //if (!CanBeChild(oldChild) || !CanBeChild(newChild))
            //    return false;

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

        public bool? IsChildrenLoaded(MetadataType? childType, IMetadataHierarchy hierarchy)
        {
            var childTypes = hierarchy.Structure[Type].ChildrenTypes.ToArray();

            if (!childTypes.Any())
                return true;

            if (childType == null)
            {
                if (_childrenMap.Count == 0)
                    return false;

                if (childTypes.Any(type => !_childrenMap.ContainsKey(type)))
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
            _fullName = GetName();
        }

        public override string ToString()
        {
            return Name;
        }

        [OnSerializing]
        public void Save(StreamingContext context)
        {
            _children = _childrenMap.ToList();
            _properties = Properties.ToList();
        }

        [OnDeserialized]
        public void Update(StreamingContext context)
        {
            _childrenMap = new Dictionary<MetadataType, List<DbObject>>();
            Properties = new Dictionary<string, object>();

            if (_children != null)
                foreach (var childType in _children)
                {
                    _childrenMap[childType.Key] = childType.Value;

                }

            if (_properties != null)

                foreach (var property in _properties)
                {
                    Properties[property.Key] = property.Value;
                }
        }

        #endregion

        #endregion
    }
}
