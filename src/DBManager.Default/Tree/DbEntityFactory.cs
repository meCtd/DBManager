using System;
using System.Collections.Generic;
using System.Data.Common;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.Tree
{
	public class DbEntityFactory
	{
		#region Fields

		private static DbEntityFactory _instance;

		private Dictionary<DbEntityType, Func<DbDataReader, DbObject>> _creator;

		protected IReadOnlyDictionary<DbEntityType, Func<DbDataReader, DbObject>> _objectCreator =>
			_creator ?? (_creator = SetCreator());

		#endregion

		public static DbEntityFactory ObjectCreator => _instance ?? (_instance = new DbEntityFactory());

		private DbEntityFactory()
		{

		}

		protected virtual Dictionary<DbEntityType, Func<DbDataReader, DbObject>> SetCreator()
		{
			Dictionary<DbEntityType, Func<DbDataReader, DbObject>> dictionary = new Dictionary<DbEntityType, Func<DbDataReader, DbObject>>
			{
				[DbEntityType.Database] = (s) => new Database(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Schema] = (s) => new Schema(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Table] = (s) => new Table(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.View] = (s) => new DbView(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Key] = (s) => new Key(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Index] = (s) => new Index(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Trigger] = (s) => new Trigger(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Constraint] = (s) => new Constraint(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Procedure] = (s) => new Procedure(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Function] = (s) => new Function(s.GetString(s.GetOrdinal(Constants.NameProperty))),
				[DbEntityType.Column] = (s) =>
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

					return new Column((s.GetString(s.GetOrdinal(Constants.NameProperty))), new DbType(s.GetString(s.GetOrdinal(Constants.TypeNameProperty)), length, precision, scale));
				},
				[DbEntityType.Parameter] = (s) =>
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

					return new Parameter((s.GetString(s.GetOrdinal(Constants.NameProperty))), new DbType(s.GetString(s.GetOrdinal(Constants.TypeNameProperty)), length, precision, scale));
				},

			};
			return dictionary;
		}

		public DbObject Create(DbDataReader reader, DbEntityType type)
		{
			return _objectCreator[type](reader);
		}
	}
}
