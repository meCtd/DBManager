using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBManager.Default.Tree.Hierarchy;
using Framework.Extensions;

namespace DBManager.Default.Tree
{
    [DataContract(Name = "db-object", IsReference = true)]
    [KnownType(typeof(DBNull))]
    public abstract class DbObject
    {
        private FullName _fullName;

        [DataMember(Name = "Children")]
        private List<KeyValuePair<MetadataType, List<DbObject>>> _children;

        [DataMember(Name = "Properties")]
        private List<KeyValuePair<string, object>> _properties;

        private Dictionary<MetadataType, List<DbObject>> _childrenMap;

        public abstract MetadataType Type { get; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        public FullName FullName => _fullName ?? (_fullName = new FullName(this));

        [DataMember(Name = "parent")]
        public DbObject Parent { get; private set; }

        public IReadOnlyList<DbObject> Children => _childrenMap.Values.SelectMany(x => x).ToList();

        public IDictionary<string, object> Properties { get; private set; }

        protected DbObject(string name)
        {
            Name = name;
            OnDeserialized(default);
        }

        public void RemoveChildren(MetadataType? type = null)
        {
            if (!type.HasValue)
                _childrenMap.Clear();

            else if (_childrenMap.ContainsKey(type.Value))
                _childrenMap.Remove(type.Value);
        }

        public void RemoveProperties()
        {
            Properties.Clear();
        }

        public void AddChild(DbObject obj)
        {
            var items = _childrenMap.ContainsKey(obj.Type)
                ? _childrenMap[obj.Type]
                : (_childrenMap[obj.Type] = new List<DbObject>());

            items.Add(obj);
            obj.Parent = this;
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

        public override string ToString()
        {
            return Name;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            _children = _childrenMap.ToList();
            _properties = Properties.ToList();
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            _childrenMap = new Dictionary<MetadataType, List<DbObject>>(_children.OrEmpty().ToDictionary(s => s.Key, s => s.Value));
            Properties = new Dictionary<string, object>(_properties.OrEmpty().ToDictionary(s => s.Key, s => s.Value));
        }
    }
}
