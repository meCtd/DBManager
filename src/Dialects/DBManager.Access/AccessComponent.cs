using System.ComponentModel.Composition;
using System.Data.Common;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.MetadataFactory;
using DBManager.Default.Normalizers;
using DBManager.Default.Printers;
using DBManager.Default.Providers;
using DBManager.Default.Tree.Hierarchy;

namespace DBManager.Access
{
	[Export(typeof(IDialectComponent))]
	class AccessComponent : IDialectComponent
	{
		public DialectType Type => DialectType.Access;

		public IPrinter Printer => throw new System.NotImplementedException();

		public IScriptProvider ScriptProvider => throw new System.NotImplementedException();

		public IMetadataHierarchy Hierarchy => throw new System.NotImplementedException();

		public IMetadataFactory ObjectFactory => throw new System.NotImplementedException();

		public NormalizerBase Normalizer => throw new System.NotImplementedException();

		public DbCommand CreateCommand() => new AccessDbCommand();

		public IConnectionData CreateConnectionData() => new AccessConnectionData();

		public DbParameter CreateParameter() => throw new System.NotImplementedException();
	}
}
