namespace DBManager.Default.Tree.DbEntities
{
	public class Server : DbObject
	{
		public override MetadataType Type => MetadataType.Server;

		public Server(string name)
			: base(name)
		{	}
	}
}
