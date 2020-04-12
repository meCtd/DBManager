using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract]
    public abstract class DataObject : DefinitionObject
    {
        protected DataObject(string name) : base(name)
        {
        }
    }
}
