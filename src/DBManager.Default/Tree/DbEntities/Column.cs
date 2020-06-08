using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "column")]
    public class Column : TypeObject
    {
        public override MetadataType Type => MetadataType.Column;

        public Column(string name) : base(name)
        {
        }

        public override string ToString()
        {
            if (DataType == null)
                return Name;

            return $"{Name} [{DataType.Name}]";
        }
    }
}
