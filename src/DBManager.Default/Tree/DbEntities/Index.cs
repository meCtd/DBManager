using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "index")]
    public class Index : DbObject
    {
        public override MetadataType Type => MetadataType.Index;

        public bool IsPrimaryKey { get; set; }

        public bool IsUniqueConstraint { get; set; }

        public Index(string name) : base(name)
        {
        }
    }
}
