using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "parameter")]
    public class Parameter : TypeObject
    {
        public override MetadataType Type => MetadataType.Parameter;

        public Parameter(string name, DbType parameterType) : base(name, parameterType)
        {
        }
    }
}
