using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.SqlClient;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;
using DBManager.SqlServer.Connection;
using DBManager.SqlServer.Loader;
using DBManager.SqlServer.Printer;


namespace DBManager.SqlServer
{
    [Export(typeof(IDialectComponent))]
    public class SqlServerComponent : IDialectComponent
    {
        internal static readonly NormalizerBase SqlNormalizer = new SqlServerNormalizer();
        
        private ILoader _loader;

        public DialectType Type => DialectType.SqlServer;

        public IPrinter Printer { get; } = new SqlServerPrinterFactory();

        public ILoader Loader => _loader ?? (_loader = new SqlServerLoader(this));

        public NormalizerBase Normalizer { get; } = SqlNormalizer;

        public DbCommand CreateCommand() => new SqlCommand();

        public DbParameter CreateParameter() => new SqlParameter();

        public IConnectionData CreateConnectionData() => new SqlServerConnectionData();
    }
}
