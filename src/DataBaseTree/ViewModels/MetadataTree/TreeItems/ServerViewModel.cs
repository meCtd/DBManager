using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class ServerViewModel : DbObjectViewModel
    {
        public override DialectType Dialect { get; }

        public ServerViewModel(Server model) : base(null, model)
        {
            Dialect = model.Dialect;
        }
    }
}
