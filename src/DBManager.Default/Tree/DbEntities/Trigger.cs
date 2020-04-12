using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "trigger")]
	public class Trigger : DefinitionObject
    {
		public override MetadataType Type => MetadataType.Trigger;

		public Trigger(string name) : base(name)
		{
		}
	}
}
