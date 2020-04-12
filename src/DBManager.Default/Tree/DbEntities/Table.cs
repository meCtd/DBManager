using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "table")]
	public class Table : DataObject
	{
		public override MetadataType Type => MetadataType.Table;

		public Table(string name) : base(name)
		{
		}
	}
}
