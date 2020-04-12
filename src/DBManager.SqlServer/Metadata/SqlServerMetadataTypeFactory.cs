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
                 [MetadataType.Database] = (s) => new Database(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Schema] = (s) => new Schema(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Table] = (s) => new Table(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.View] = (s) => new View(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Key] = (s) => new Key(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Index] = (s) => new Index(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Trigger] = (s) => new Trigger(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Constraint] = (s) => new Constraint(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Procedure] = (s) => new Procedure(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Function] = (s) => new Function(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Column] = (s) => new Column(s.GetString(s.GetOrdinal(Constants.Name))),
                 [MetadataType.Parameter] = (s) => new Parameter(s.GetString(s.GetOrdinal(Constants.Name))),
             };

        public DbObject Create(DbDataReader reader, MetadataType type)
        {
            if (!_dictionary.TryGetValue(type, out var func))
                throw new ArgumentException(nameof(type));

            return func(reader);
        }
    }
}
