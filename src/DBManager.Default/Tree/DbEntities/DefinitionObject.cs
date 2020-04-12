using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    public abstract class DefinitionObject : DbObject
    {
        [DataMember(Name = "definition", EmitDefaultValue = false)]
        public string Definition { get; set; }

        protected DefinitionObject(string name) : base(name)
        {
        }
    }
}
