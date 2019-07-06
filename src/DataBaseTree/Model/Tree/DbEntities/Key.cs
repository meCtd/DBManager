using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "key")]

	public class Key : DbObject
	{
		public override DbEntityType Type => DbEntityType.Key;

		public override bool CanHaveDefinition => false;

		public Key(string name) : base(name)
		{
		}

	}
}
