using System.Data.Common;

using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

using Framework.Extensions;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerParameterLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Parameter;

        public SqlServerParameterLoader(IDialectComponent components)
            : base(components)
        { }

        protected override DbObject CreateObject(DbDataReader reader)
        {
            var name = reader.Get<string>(SqlServerConstants.Name);
            return new Parameter(string.IsNullOrEmpty(name) ? SqlServerConstants.ReturnValue : name);
        }
    }
}
