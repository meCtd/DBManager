using System;
using System.Collections.Generic;
using System.Data.Common;

using DBManager.Default;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Metadata
{
	class MsSqlMetadataTypeFactory : MetadataTypeFactory
	{
		public override DialectType Dialect => DialectType.MsSql;

		private MsSqlMetadataTypeFactory() 
			: base(new Dictionary<MetadataType, Func<DbDataReader, DbObject>>
		{
				[MetadataType.Database] = (s) => new MsSqlDatabase(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Schema] = (s) => new MsSqlSchema(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Table] = (s) => new MsSqlTable(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.View] = (s) => new MsSqlView(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Key] = (s) => new MsSqlKey(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Index] = (s) => new MsSqlIndex(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Trigger] = (s) => new MsSqlTrigger(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Constraint] = (s) => new MsSqlConstraint(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Procedure] = (s) => new MsSqlProcedure(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[MetadataType.Function] = (s) => new MsSqlFunction(s.GetString(s.GetOrdinal(Constants.NameProperty))),
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

					return new MsSqlColumn((s.GetString(s.GetOrdinal(Constants.NameProperty))), new DbType(s.GetString(s.GetOrdinal(Constants.TypeNameProperty)), length, precision, scale));
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

					return new MsSqlParameter((s.GetString(s.GetOrdinal(Constants.NameProperty))), new DbType(s.GetString(s.GetOrdinal(Constants.TypeNameProperty)), length, precision, scale));
				},
			})
		{ }
	}
}
