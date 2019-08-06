using DBManager.Default.Loaders;
using DBManager.Default.Printers;
using DBManager.Default.Tree;

namespace DBManager.Default
{
    //TODO: Base dialect components that need to work with
    public interface IDialectComponent
    {
        IPrinterFactory Printer { get; }

        IObjectLoader Loader { get; }

        Hierarchy GetHierarchy();
    }
}
