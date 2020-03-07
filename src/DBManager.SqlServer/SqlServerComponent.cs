using System.Data.Common;
using System.Data.SqlClient;
using DBManager.Default;
using DBManager.Default.MetadataFactory;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;
using DBManager.Default.Providers;
using DBManager.Default.Tree.Hierarchy;
using DBManager.SqlServer.Metadata;
using DBManager.SqlServer.Printer;
using DBManager.SqlServer.Provider;

namespace DBManager.SqlServer
{
    public class SqlServerComponent : IDialectComponent
    {
        internal static readonly NormalizerBase SqlNormalizer = new SqlServerNormalizer();

        public IPrinter Printer { get; } = new SqlServerPrinterFactory();

        public IScriptProvider ScriptProvider { get; } = new SqlServerScriptProvider();

        public IMetadataHierarchy Hierarchy { get; } = new SqlServerHierarchy();

        public IMetadataFactory ObjectFactory { get; } = new SqlServerMetadataTypeFactory();

        public NormalizerBase Normalizer { get; } = SqlNormalizer;

        public DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public DbParameter CreateParameter()
        {
            return new SqlParameter();
        }
    }
}
