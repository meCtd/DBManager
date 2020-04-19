using System.ComponentModel.Composition;
using System.Data.Common;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Access.Loader;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loader;
using DBManager.Default.Normalizers;
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

        public NormalizerBase Normalizer => throw new System.NotImplementedException();

        public DbCommand CreateCommand() => new AccessDbCommand();

        public IConnectionData CreateConnectionData() => new AccessConnectionData();

        public DbParameter CreateParameter() => throw new System.NotImplementedException();
    }
}
