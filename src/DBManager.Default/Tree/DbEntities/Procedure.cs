using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "procedure")]
	public abstract class Procedure : Routine
	{
		public override MetadataType Type => MetadataType.Procedure;

		public Procedure(string name) : base(name)
		{
		}
	}
}
