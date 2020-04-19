using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Tree.DbEntities;

using Ninject;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class ServerViewModel : DbObjectViewModel
    {
        public override DialectType Dialect { get; }

        public IConnectionData ConnectionData => ((Server)Model).ConnectionData;

        protected override IDialectComponent Components { get; }

        public ServerViewModel(Server model) : base(null, model)
        {
            Dialect = model.Dialect;

            Components = Context.Resolver.Get<IDialectComponent>(Dialect.ToString());
        }
    }
}
