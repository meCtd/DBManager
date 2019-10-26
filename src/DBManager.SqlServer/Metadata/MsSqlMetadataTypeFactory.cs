using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using DBManager.Default;
using DBManager.Default.MetadataFactory;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
    //TODO MAKE SINGLETONE
    class MsSqlMetadataTypeFactory : IMetadataFactory
    {
        public DialectType Dialect => DialectType.MsSql;

        private static readonly Dictionary<MetadataType, Func<DbDataReader, DbObject>> _dictionary =
            new Dictionary<MetadataType, Func<DbDataReader, DbObject>>()
            {
                [MetadataType.Database] = (s) => new Database(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Schema] = (s) => new Schema(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Table] = (s) => new Table(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.View] = (s) => new View(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Key] = (s) => new Key(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Index] = (s) => new Index(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Trigger] = (s) => new Trigger(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Constraint] = (s) => new Constraint(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Procedure] = (s) => new Procedure(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Function] = (s) => new Function(s.GetString(s.GetOrdinal(Constants.NameProperty))),
                [MetadataType.Column] = (s) =>
                {
                    int? length = null;
                    int? precision = null;
                    int? scale = null;
                    if (!s.IsDBNull(s.GetOrdinal(Constants.PrecisionProperty)))
                        precision = s.GetByte(s.GetOrdinal(Constants.PrecisionProperty));

                    if (!s.IsDBNull(s.GetOrdinal(Constants.ScaleProperty)))
                        scale = s.GetByte(s.GetOrdinal(Constants.ScaleProperty));

                    if (!s.IsDBNull(s.GetOrdinal(Constants.MaxLengthProperty)))
                        length = s.GetInt16(s.GetOrdinal(Constants.MaxLengthProperty));

                    return new Column((s.GetString(s.GetOrdinal(Constants.NameProperty))),
                        new DbType(s.GetString(s.GetOrdinal(Constants.TypeNameProperty)), length, precision, scale));
                },
                [MetadataType.Parameter] = (s) =>
                {
                    int? length = null;
                    int? precision = null;
                    int? scale = null;
                    if (!s.IsDBNull(s.GetOrdinal(Constants.PrecisionProperty)))
                        precision = s.GetByte(s.GetOrdinal(Constants.PrecisionProperty));

                    if (!s.IsDBNull(s.GetOrdinal(Constants.ScaleProperty)))
                        scale = s.GetByte(s.GetOrdinal(Constants.ScaleProperty));

                    if (!s.IsDBNull(s.GetOrdinal(Constants.MaxLengthProperty)))
                        length = s.GetInt16(s.GetOrdinal(Constants.MaxLengthProperty));

                    return new Parameter((s.GetString(s.GetOrdinal(Constants.NameProperty))),
                        new DbType(s.GetString(s.GetOrdinal(Constants.TypeNameProperty)), length, precision, scale));
                }
            };

        public DbObject Create(DbDataReader reader, MetadataType type)
        {
            if (!_dictionary.TryGetValue(type,out var func))
                throw new ArgumentException(nameof(type));

            return func(reader);
        }
    }
}
