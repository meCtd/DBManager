using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
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
