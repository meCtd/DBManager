using System.ComponentModel.Composition;

using DBManager.Access.Loader;

using DBManager.Default;
using DBManager.Default.Execution;
using DBManager.Default.Loader;
using DBManager.Default.Printers;


namespace DBManager.Access
{
    [Export(typeof(IDialectComponent))]
    public class AccessComponent : IDialectComponent
    {
        private ILoader _loader;

        public DialectType Type => DialectType.Access;

        public IPrinter Printer => throw new System.NotImplementedException();

        public ILoader Loader => _loader ?? (_loader = new AccessLoader(this));

        public IComponentCreator Creator { get; } = new AccessCreator();

        public IScriptExecutor Executor { get; }
    }
}
