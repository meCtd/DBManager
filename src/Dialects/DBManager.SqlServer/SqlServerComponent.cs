using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.SqlClient;

using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Execution;
using DBManager.Default.Loader;
using DBManager.Default.Printers;
using DBManager.SqlServer.Connection;
using DBManager.SqlServer.Loader;
using DBManager.SqlServer.Printer;


namespace DBManager.SqlServer
{
    [Export(typeof(IDialectComponent))]
    public class SqlServerComponent : IDialectComponent
    {
        private ILoader _loader;

        public DialectType Type => DialectType.SqlServer;

        public IPrinter Printer { get; } = new SqlServerPrinterFactory();

        public ILoader Loader => _loader ?? (_loader = new SqlServerLoader(this));

        public IComponentCreator Creator { get; } = new SqlServerCreator();
    }
}
