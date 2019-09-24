using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "trigger")]
	public abstract class Trigger : DbObject
	{
		public override bool CanHaveDefinition => true;

		public override MetadataType Type => MetadataType.Trigger;

		public Trigger(string name) : base(name)
		{
		}
	}
}
