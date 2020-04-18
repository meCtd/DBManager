using System.Threading;
using System.Threading.Tasks;

using DBManager.Application.ViewModels.General;
using DBManager.Default;
using DBManager.Default.Loader;
using DBManager.Default.Tree;

using Framework.Extensions;

using Ninject;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class DbObjectViewModel : MetadataViewModelBase
    {
        public DbObject Model { get; }

        public override string Name => Model.ToString();

        public override MetadataType Type => Model.Type;

        public DbObjectViewModel(MetadataViewModelBase parent, DbObject model)
            : base(parent, CanHaveChildren(parent, model))
        {
            Model = model;
        }

        private static bool CanHaveChildren(MetadataViewModelBase parent, DbObject model)
        {
            if (parent is null)
                return true;

            return AppContext.Current.Resolver.Get<IDialectComponent>(parent.Dialect.ToString()).Hierarchy.Structure.GetValueOrDefault(model.Type)?.HasChildren ?? false;
        }

        protected override void RemoveChildrenFromModel()
        {
            Model.RemoveChildren();
        }

        protected override async Task LoadChildren()
        {

            var node = Components.Hierarchy.Structure[Type];

            if (node.NeedCategory)
                node.ChildrenTypes.ForEach(type => Children.Add(new CategoryViewModel(Model, this, type)));
            else
            {
                var loadingContext = new LoadingContext(((ServerViewModel)Root).ConnectionData, CancellationToken.None);
                await Components.Loader.LoadChildrenAsync(loadingContext, Model);

                Model.Children.ForEach(s => Children.Add(new DbObjectViewModel(this, s)));
            }
        }
    }
}
