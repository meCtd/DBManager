using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "index")]
	public abstract class Index : DbObject
	{
		public override MetadataType Type => MetadataType.Index;

		public override bool CanHaveDefinition => false;

		public Index(string name) : base(name)
		{
		}
	}
}
