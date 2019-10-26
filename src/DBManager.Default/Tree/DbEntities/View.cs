using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "view")]
	public class View : TableData
	{
		public override MetadataType Type => MetadataType.View;

		public override bool CanHaveDefinition => true;

		public View(string name) : base(name)
		{
		}

		
	}
}
