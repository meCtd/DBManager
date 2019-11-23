using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "view")]
	public class View : DataObject
	{
		public override MetadataType Type => MetadataType.View;

		public View(string name) : base(name)
		{
		}
	}
}
