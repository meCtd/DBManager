using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
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
