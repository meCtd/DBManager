using System.Runtime.Serialization;


namespace DBManager.Default.DataBaseConnection
{
    [DataContract(Name = "server-info")]
    public class ServerInfo
    {
        [DataMember(Name = "version")]
        public long Version { get; set; }
    }
}
