using System.Data.Common;

using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;
using DBManager.Default.Tree.Hierarchy;


namespace DBManager.Default
{
    public interface IDialectComponent
    {
        DialectType Type { get; }

        IPrinter Printer { get; }

        //IScriptProvider ScriptProvider { get; }
        ILoader Loader { get; }

        IMetadataHierarchy Hierarchy { get; }

        NormalizerBase Normalizer { get; }

        DbCommand CreateCommand();

        DbParameter CreateParameter();

        IConnectionData CreateConnectionData();
    }
}
