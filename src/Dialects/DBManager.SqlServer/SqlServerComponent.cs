using System.ComponentModel.Composition;

using DBManager.Default;
using DBManager.Default.Execution;
using DBManager.Default.Loader;
using DBManager.Default.Printers;
using DBManager.SqlServer.Execution;
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

        public IComponentCreator Creator => SqlServerCreator.Instance;

        public IScriptExecutor Executor { get; } = new SqlServerScriptExecutor();
    }
}
