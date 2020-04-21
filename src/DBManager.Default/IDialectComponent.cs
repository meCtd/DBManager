using DBManager.Default.Execution;
using DBManager.Default.Loader;
using DBManager.Default.Printers;

namespace DBManager.Default
{
    public interface IDialectComponent
    {
        DialectType Type { get; }

        IPrinter Printer { get; }

        ILoader Loader { get; }

        IComponentCreator Creator { get; }

        IScriptExecutor Executor { get; }
    }
}
