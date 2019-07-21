using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "constraint")]
	public class Constraint : DbObject
	{
		public override DbEntityType Type => DbEntityType.Constraint;

		public override bool CanHaveDefinition => false;

		public Constraint(string name) : base(name)
		{
		}
	}
}
