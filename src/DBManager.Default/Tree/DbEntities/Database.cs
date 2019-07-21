using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "database")]
	[KnownType(typeof(Schema))]
	public class Database : DbObject
	{
		public override DbEntityType Type => DbEntityType.Database;

		public override bool CanHaveDefinition => false;

		public Database(string name) : base(name)
		{
		}

		protected override bool CanBeChild(DbObject obj)
		{
			return obj.Type == DbEntityType.Schema;
		}


	}
}
