using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "database")]
    public class Database : DefinitionObject
    {
        public override MetadataType Type => MetadataType.Database;

        public Database(string name) : base(name)
        {
        }
    }
}
