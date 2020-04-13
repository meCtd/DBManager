using System;
using System.Collections.Generic;
using System.Data.Common;

using DBManager.Default;
using DBManager.Default.MetadataFactory;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;


namespace DBManager.SqlServer.Metadata
{
    internal class SqlServerMetadataTypeFactory : IMetadataFactory
    {
        public DialectType Dialect => DialectType.SqlServer;

        private static readonly Dictionary<MetadataType, Func<DbDataReader, DbObject>> _dictionary =
             new Dictionary<MetadataType, Func<DbDataReader, DbObject>>()
             {
                 [MetadataType.Database] = s => new Database(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Schema] = s => new Schema(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Table] = s => new Table(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.View] = s => new View(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Index] = s =>
                 {
                     var index = new Index(s.GetString(s.GetOrdinal(Constants.Name)))
                     {
                         IsPrimaryKey = s.GetBoolean(s.GetOrdinal(Constants.IsPrimaryKey)),
                         IsUniqueConstraint = s.GetBoolean(s.GetOrdinal(Constants.IsUniqueConstraint))
                     };

                     return index;
                 },
                 [MetadataType.Trigger] = s => new Trigger(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Constraint] = s =>
                 {
                     var constraint = new Constraint(s.GetString(s.GetOrdinal(Constants.Name)));

                     switch (s.GetString(s.GetOrdinal(Constants.ConstraintType)))
                     {
                         case Constants.PrimaryKey:
                             constraint.ConstraintType = ConstraintType.PrimaryKey;
                             break;

                         case Constants.ForeignKey:
                             constraint.ConstraintType = ConstraintType.ForeignKey;
                             break;

                         case Constants.CheckConstraint:
                             constraint.ConstraintType = ConstraintType.CheckConstraint;
                             break;

                         case Constants.UniqueConstraint:
                             constraint.ConstraintType = ConstraintType.UniqueConstraint;
                             break;
                     }

                     return constraint;
                 },
                 [MetadataType.Procedure] = s => new Procedure(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Function] = s => new Function(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Column] = s => new Column(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Parameter] = s =>
                 {
                     var name = s.GetString(s.GetOrdinal(Constants.Name));
                     return new Parameter(!string.IsNullOrEmpty(name) ? name : Constants.ReturnValue);
                 },
             };

        public DbObject Create(DbDataReader reader, MetadataType type)
        {
            if (!_dictionary.TryGetValue(type, out var func))
                throw new ArgumentException(nameof(type));

            return func(reader);
        }
    }
}
