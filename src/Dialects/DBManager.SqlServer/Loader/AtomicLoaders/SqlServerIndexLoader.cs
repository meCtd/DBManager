using System.Data.Common;
using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;
using Framework.Extensions;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    internal class SqlServerIndexLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Index;

        public SqlServerIndexLoader(IDialectComponent components)
            : base(components)
        { }

        protected override DbObject CreateObject(DbDataReader reader)
        {
            var index = (Index)base.CreateObject(reader);

            index.IsPrimaryKey = reader.Get<bool>(SqlServerConstants.IsPrimaryKey);
            index.IsUniqueConstraint = reader.Get<bool>(SqlServerConstants.IsUniqueConstraint);

            return index;
        }
    }
}
