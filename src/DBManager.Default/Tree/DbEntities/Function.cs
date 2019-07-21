using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "function")]
	public class Function : Routine
	{
		public override DbEntityType Type => DbEntityType.Function;

		public Function(string name) : base(name)
		{
		}
	}
}
