using System.Data.Common;
using DBManager.Default;
using DBManager.Default.Loader.Sql;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;
using Framework.Extensions;

namespace DBManager.SqlServer.Loader.AtomicLoaders
{
    class SqlServerConstraintLoader : BaseAtomicSqlLoader
    {
        public override MetadataType Type => MetadataType.Constraint;

        public SqlServerConstraintLoader(IDialectComponent components)
            : base(components)
        { }

        protected override DbObject CreateObject(DbDataReader reader)
        {
            var constraint = (Constraint)base.CreateObject(reader);

            switch (reader.Get<string>(SqlServerConstants.ConstraintType))
            {
                case SqlServerConstants.PrimaryKey:
                    constraint.ConstraintType = ConstraintType.PrimaryKey;
                    break;

                case SqlServerConstants.ForeignKey:
                    constraint.ConstraintType = ConstraintType.ForeignKey;
                    break;

                case SqlServerConstants.CheckConstraint:
                    constraint.ConstraintType = ConstraintType.CheckConstraint;
                    break;

                case SqlServerConstants.UniqueConstraint:
                    constraint.ConstraintType = ConstraintType.UniqueConstraint;
                    break;
            }

            return constraint;
        }
    }
}
