using System.Data.Common;
using DBManager.Default.Tree;

namespace DBManager.Default.MetadataFactory
{
    public interface IMetadataFactory
    {
        DialectType Dialect { get; }
        DbObject Create(DbDataReader reader, MetadataType type);
    }
}
