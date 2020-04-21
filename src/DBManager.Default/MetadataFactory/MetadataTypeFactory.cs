using System;
using System.Collections.Generic;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.MetadataFactory
{
    public class MetadataTypeFactory
    {
        public static MetadataTypeFactory Instance { get; } = new MetadataTypeFactory();
        
        private MetadataTypeFactory()
        {
        }

        private static readonly Dictionary<MetadataType, Func<string, DbObject>> _dictionary =
             new Dictionary<MetadataType, Func<string, DbObject>>
             {
                 [MetadataType.Database] = name => new Database(name),
                 [MetadataType.Schema] = name => new Schema(name),
                 [MetadataType.Table] = name => new Table(name),
                 [MetadataType.View] = name => new View(name),
                 [MetadataType.Index] = name => new Index(name),
                 [MetadataType.Trigger] = name => new Trigger(name),
                 [MetadataType.Constraint] = name => new Constraint(name),
                 [MetadataType.Procedure] = name => new Procedure(name),
                 [MetadataType.Function] = name => new Function(name),
                 [MetadataType.Column] = name => new Column(name),
                 [MetadataType.Parameter] = name => new Parameter(name)
             };

        public DbObject Create(MetadataType type, string name)
        {
            if (!_dictionary.TryGetValue(type, out var func))
                throw new ArgumentException(nameof(type));

            return func(name);
        }
    }
}
