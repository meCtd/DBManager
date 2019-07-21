using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "procedure")]
	public class Procedure : Routine
	{
		public override DbEntityType Type => DbEntityType.Procedure;

		public Procedure(string name) : base(name)
		{
		}
	}
}
