using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "server")]
	[KnownType(typeof(Database))]
	public abstract class Server : DbObject
	{
		public override MetadataType Type => MetadataType.Server;
		public override bool CanHaveDefinition => false;

		public Server(string name) : base(name)
		{
		}
	}
}
