using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Providers;
using DBManager.Default.Tree;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.Default.Loaders
{
    [DataContract(Name = "Loader", IsReference = true)]
    [KnownType(nameof(KnownType))]
    public abstract class ObjectLoader : IObjectLoader
    {
        protected ScriptProvider _provider;

        //TODO: TO project
        [DataMember(Name = "ConnectionData")]
        public ConnectionData Connection { get; set; }

        public MetadataHierarchy Hierarchy => MetadataHierarchy.Instance;

        public abstract Task LoadChildrenAsync(DbObject obj);

        public abstract Task LoadChildrenAsync(DbObject obj, MetadataType childType);

        public abstract Task LoadPropertiesAsync(DbObject obj);

        protected ObjectLoader(ConnectionData connection, ScriptProvider provider)
        {
            Connection = connection;
            _provider = provider;
        }

        private static IEnumerable<Type> KnownType()
        {
            return typeof(ConnectionData).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ConnectionData)));
        }
    }
}
