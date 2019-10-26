using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "table")]
	[KnownType(typeof(Constraint))]
	[KnownType(typeof(Key))]
	public class Table : TableData
	{
		public override MetadataType Type => MetadataType.Table;

		public Table(string name) : base(name)
		{
		}
	}
}
