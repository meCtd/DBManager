using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "database")]
    [KnownType(typeof(Schema))]
    public class Database : DbObject
    {
        public override MetadataType Type => MetadataType.Database;

        public override bool CanHaveDefinition => false;

        public Database(string name) : base(name)
        {
        }
    }
}
