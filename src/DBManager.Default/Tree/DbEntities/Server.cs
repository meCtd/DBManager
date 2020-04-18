using DBManager.Default.DataBaseConnection;

namespace DBManager.Default.Tree.DbEntities
{
    public class Server : DbObject
    {
        public override MetadataType Type => MetadataType.Server;

        public DialectType Dialect { get; }
        public IConnectionData ConnectionData { get; }

        public Server(IConnectionData connectionData, string name, DialectType dialect)
            : base(name)
        {
            ConnectionData = connectionData;
            Dialect = dialect;
        }
    }
}
