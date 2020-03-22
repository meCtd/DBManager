namespace DBManager.Default.Tree
{
    public class Server : DbObject
    {
        public Server(string name) : base(name)
        {
        }

        public override MetadataType Type => MetadataType.Server;
    }
}
