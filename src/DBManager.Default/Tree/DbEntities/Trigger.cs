using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "trigger")]
	public class Trigger : DbObject
	{
		public override bool CanHaveDefinition => true;

		public override DbEntityType Type => DbEntityType.Trigger;

		public Trigger(string name) : base(name)
		{
		}
	}
}
