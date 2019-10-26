using System.Threading.Tasks;
using DBManager.Default.Tree;

namespace DBManager.Default.Loaders
{
    public interface IObjectLoader
    {

        bool IsOnline { get; set; }

        Task LoadChildrenAsync(DbObject obj);

        Task LoadChildrenAsync(DbObject obj, MetadataType childType);

        Task LoadPropertiesAsync(DbObject obj);
    }
}
