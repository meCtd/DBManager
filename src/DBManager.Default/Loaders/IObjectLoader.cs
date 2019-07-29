using System.Threading.Tasks;
using DataBaseTree.Model.DataBaseConnection;
using DataBaseTree.Model.Tree;

namespace DBManager.Default.Loaders
{
    public interface IObjectLoader
    {
        ConnectionData Connection { get; set; }

        Task LoadChildrenAsync(DbObject obj);

        Task LoadChildrenAsync(DbObject obj, DbEntityType childType);

        Task LoadPropertiesAsync(DbObject obj);
    }
}
