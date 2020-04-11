using System.Data;
using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "parameter")]
    public class Parameter : TypeObject
    {
        public override MetadataType Type => MetadataType.Parameter;

        [DataMember(Name = "parameter-direction")]
        public ParameterDirection Directon { get; set; }

        public Parameter(string name) : base(name)
        {
        }
    }
}
