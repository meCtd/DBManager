using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.SqlClient;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;
using DBManager.Default.MetadataFactory;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;
using DBManager.Default.Providers;
using DBManager.Default.Tree.Hierarchy;

using DBManager.SqlServer.Connection;
using DBManager.SqlServer.Loader;
using DBManager.SqlServer.Metadata;
using DBManager.SqlServer.Printer;
using DBManager.SqlServer.Provider;


namespace DBManager.SqlServer
{
    [Export(typeof(IDialectComponent))]
    public class SqlServerComponent : IDialectComponent
    {
        private ILoader _loader;

        internal static readonly NormalizerBase SqlNormalizer = new SqlServerNormalizer();

        public DialectType Type => DialectType.SqlServer;

        public IPrinter Printer { get; } = new SqlServerPrinterFactory();

        public ILoader Loader => _loader ?? new SqlServerLoader(this);

        public IMetadataHierarchy Hierarchy => Loader.Hierarchy;

        public NormalizerBase Normalizer { get; } = SqlNormalizer;

        public DbCommand CreateCommand() => new SqlCommand();

        public DbParameter CreateParameter() => new SqlParameter();

        public IConnectionData CreateConnectionData() => new SqlServerConnectionData();
    }
}
