using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "server")]
	[KnownType(typeof(Database))]
	public class Server : DbObject
	{
		public override DbEntityType Type => DbEntityType.Server;
		public override bool CanHaveDefinition => false;

		public Server(string name) : base(name)
		{
		}

		protected override bool CanBeChild(DbObject obj)
		{
			return obj.Type == DbEntityType.Database;
		}
	}
}
