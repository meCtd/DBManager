using System.Data.Common;
using DBManager.Default.MetadataFactory;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;
using DBManager.Default.Providers;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.Default
{
    public interface IDialectComponent
    {
        IPrinter Printer { get; }

        IScriptProvider ScriptProvider { get; }

        IMetadataHierarchy Hierarchy { get; }

        IMetadataFactory ObjectFactory { get; }

        NormalizerBase Normalizer { get; }

        DbCommand CreateCommand();
        DbParameter CreateParameter();
    }
}
