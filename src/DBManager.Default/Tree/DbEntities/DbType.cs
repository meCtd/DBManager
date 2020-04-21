using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "db-type")]
    public class DbType : DbObject
    {
        public override MetadataType Type => MetadataType.Type;

        [DataMember(Name = "length")]
        public int? Length { get; set; }

        [DataMember(Name = "precision")]
        public int? Precision { get; set; }

        [DataMember(Name = "scale")]
        public int? Scale { get; set; }

        public DbType(string name) : base(name)
        {
        }
    }
}
