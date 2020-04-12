namespace DBManager.Default.Tree.DbEntities
{
    public class Server : DbObject
    {
        public override MetadataType Type => MetadataType.Server;

        public DialectType Dialect { get; }

        public Server(string name, DialectType dialect)
            : base(name)
        {
            Dialect = dialect;
        }
    }
}
