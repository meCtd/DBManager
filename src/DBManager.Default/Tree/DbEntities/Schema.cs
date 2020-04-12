
using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "schema")]
	public class Schema : DefinitionObject
    {
		public override MetadataType Type => MetadataType.Schema;

		public Schema(string name) : base(name)
		{
		}
	}
}
