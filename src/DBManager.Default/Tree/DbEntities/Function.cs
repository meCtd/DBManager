using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "function")]
	public class Function : Routine
	{
		public override MetadataType Type => MetadataType.Function;

		public Function(string name) : base(name)
		{
		}
	}
}
