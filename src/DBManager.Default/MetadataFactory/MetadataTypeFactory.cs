using System;
using System.Collections.Generic;

using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;


namespace DBManager.Default.MetadataFactory
{
    public class MetadataTypeFactory : IMetadataFactory
    {
        public static MetadataTypeFactory Instance { get; } = new MetadataTypeFactory();
        
        private MetadataTypeFactory()
        {
        }

        private static readonly Dictionary<MetadataType, Func<string, DbObject>> _dictionary =
             new Dictionary<MetadataType, Func<string, DbObject>>()
             {
                 [MetadataType.Database] = name => new Database(name),
                 [MetadataType.Schema] = name => new Schema(name),
                 [MetadataType.Table] = name => new Table(name),
                 [MetadataType.View] = name => new View(name),
                 [MetadataType.Index] = name => new Index(name),
                 //{
                 //    var index = new Index(s.GetString(s.GetOrdinal(Constants.Name)))
                 //    {
                 //        IsPrimaryKey = s.GetBoolean(s.GetOrdinal(Constants.IsPrimaryKey)),
                 //        IsUniqueConstraint = s.GetBoolean(s.GetOrdinal(Constants.IsUniqueConstraint))
                 //    };

                 //    return index;
                 //},
                 [MetadataType.Trigger] = name => new Trigger(name),
                 [MetadataType.Constraint] = name => new Constraint(name),
                 //{
                 //    var constraint = new Constraint(s.GetString(s.GetOrdinal(Constants.Name)));

                 //    switch (s.GetString(s.GetOrdinal(Constants.ConstraintType)))
                 //    {
                 //        case Constants.PrimaryKey:
                 //            constraint.ConstraintType = ConstraintType.PrimaryKey;
                 //            break;

                 //        case Constants.ForeignKey:
                 //            constraint.ConstraintType = ConstraintType.ForeignKey;
                 //            break;

                 //        case Constants.CheckConstraint:
                 //            constraint.ConstraintType = ConstraintType.CheckConstraint;
                 //            break;

                 //        case Constants.UniqueConstraint:
                 //            constraint.ConstraintType = ConstraintType.UniqueConstraint;
                 //            break;
                 //    }

                 //    return constraint;
                 //},
                 [MetadataType.Procedure] = name => new Procedure(name),
                 [MetadataType.Function] = name => new Function(name),
                 [MetadataType.Column] = name => new Column(name),
                 [MetadataType.Parameter] = name => new Parameter(name),
                 //{
                 //    var name = s.GetString(s.GetOrdinal(Constants.Name));
                 //    return new Parameter(!string.IsNullOrEmpty(name) ? name : Constants.ReturnValue);
                 //},
             };

        public DbObject Create(MetadataType type, string name)
        {
            if (!_dictionary.TryGetValue(type, out var func))
                throw new ArgumentException(nameof(type));

            return func(name);
        }
    }
}
