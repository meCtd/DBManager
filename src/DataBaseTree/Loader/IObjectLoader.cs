using System.Threading;
using System.Threading.Tasks;
using DBManager.Default.Tree;

namespace DBManager.Application.Loader
{
    public interface IObjectLoader
    {
        Task LoadServerProperties(CancellationToken token);

        Task LoadChildrenAsync(DbObject obj, CancellationToken token);

        Task LoadChildrenAsync(DbObject obj, MetadataType childType, CancellationToken token);

        Task LoadPropertiesAsync(DbObject obj, CancellationToken token);
    }
}
