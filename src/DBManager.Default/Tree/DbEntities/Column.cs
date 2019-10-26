using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "column")]
    public class Column : TypeObject
    {
        public override MetadataType Type => MetadataType.Column;

        public override bool CanHaveDefinition => false;

        public Column(string name, DbType columnType) : base(name, columnType)
        {
        }
    }
}
