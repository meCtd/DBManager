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
        private FullName _fullName;

        [DataMember(Name = "Children")]
        private Dictionary<MetadataType, List<DbObject>> _childrenMap = new Dictionary<MetadataType, List<DbObject>>();

        public abstract MetadataType Type { get; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        public FullName FullName => _fullName ?? (_fullName = new FullName(this));

        [DataMember(Name = "parent")]
        public DbObject Parent { get; private set; }

        public IReadOnlyList<DbObject> Children => _childrenMap.Values.SelectMany(x => x).ToList();

        [DataMember(Name = "Properties")]
        public IDictionary<string, object> Properties { get; private set; }

        protected DbObject(string name)
        {
            Name = name;
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

        public T GetFirstAncestorOf<T>() where T : DbObject
        {
            var current = Parent;

            while (Parent != null)
            {
                if (current.GetType() == typeof(T))
                    return (T)current;

                current = current.Parent;
            }

            return null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
