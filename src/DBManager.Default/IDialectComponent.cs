using System.Data.Common;

using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;


namespace DBManager.Default
{
    public interface IDialectComponent
    {
        DialectType Type { get; }

        IPrinter Printer { get; }

        ILoader Loader { get; }

        NormalizerBase Normalizer { get; }

        DbCommand CreateCommand();

        DbParameter CreateParameter();

        IConnectionData CreateConnectionData();
    }
}
