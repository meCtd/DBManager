using System.Data.Common;
using System.Data.SqlClient;
using DBManager.Default;
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
        public IPrinter Printer { get; } = new MsSqlPrinterFactory();

        public IScriptProvider ScriptProvider { get; } = new MsSqlScriptProvider();

        public IMetadataHierarchy Hierarchy { get; } = MsSqlHierarchy.Instance;

        public NormalizerBase Normalizer { get; } = SqlServerNormalizer.Instance;

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
