using System.ComponentModel.Composition;
using DBManager.Access.Execution;
using DBManager.Access.Loader;
using DBManager.Default;
using DBManager.Default.Execution;
using DBManager.Default.Loader;
using DBManager.Default.Printers;
using DBManager.SqlServer.Printer;

namespace DBManager.Access
{
    [Export(typeof(IDialectComponent))]
    public class AccessComponent : IDialectComponent
    {
        private ILoader _loader;

        public DialectType Type => DialectType.Access;

        public IPrinter Printer { get; } = new AccessPrinterFactory();

        public ILoader Loader => _loader ?? (_loader = new AccessLoader(this));

        public IComponentCreator Creator { get; } = AccessCreator.Instance;

        public IScriptExecutor Executor { get; } = AccessExecutor.Instance;
    }
}
