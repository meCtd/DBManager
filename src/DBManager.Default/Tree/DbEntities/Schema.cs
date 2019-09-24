
using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "schema")]
	[KnownType(typeof(Procedure))]
	[KnownType(typeof(Function))]
	[KnownType(typeof(Table))]
	[KnownType(typeof(View))]
	public abstract class Schema : DbObject
	{
		public override MetadataType Type => MetadataType.Schema;

		public override bool CanHaveDefinition => false;

		public Schema(string name) : base(name)
		{
		}
	}
}
