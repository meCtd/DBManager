using System.Data.Common;
using DBManager.Default.Printers;
using DBManager.Default.Providers;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.Default
{
    //TODO: Base dialect components that need to work with
    public interface IDialectComponent
    {
        IPrinterFactory Printer { get; }

        IScriptProvider ScriptProvider { get; }

        IMetadataHierarchy Hierarchy { get; }

        DbCommand CreateCommand();
        DbParameter CreateParameter();
    }
}
