using System.Data.Common;
using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;
using Framework.Extensions;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerColumnLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Column;

        public SqlServerColumnLoader(IDialectComponent components)
            : base(components)
        { }

        protected override DbObject CreateObject(DbDataReader reader)
        {
            var instance = (Column)base.CreateObject(reader);

            instance.DataType = _components.Loader.DataTypeFactory.CreateTypeDescriptor(reader.Get<string>("DATA_TYPE"));

            return instance;
        }
    }
}
