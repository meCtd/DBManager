using System.Threading;
using System.Threading.Tasks;

using DBManager.Default;
using DBManager.Default.Loaders;
using DBManager.Default.Tree;

using Framework.Extensions;

using Ninject;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class DbObjectViewModel : MetadataViewModelBase
    {
        public DbObject Model { get; }

        public override string ObjectName => Model.ToString();

        public override MetadataType Type => Model.Type;

        public DbObjectViewModel(MetadataViewModelBase parent, DbObject model) : base(parent, Resolver.Get<IDialectComponent>().Hierarchy.Structure[model.Type].HasChildren)
        {
            Model = model;
        }

        protected override void RemoveChildrenFromModel()
        {
            Model.RemoveChildren();
        }

        protected override async Task LoadChildren()
        {
            var node = Resolver.Get<IDialectComponent>().Hierarchy.Structure[Type];

            if (node.NeedCategory)
                node.ChildrenTypes.ForEach(type => Children.Add(new CategoryViewModel(Model, this, type)));

            else
            {
                await GetLoader().LoadChildrenAsync(Model, CancellationToken.None);

                Model.Children.ForEach(s => Children.Add(new DbObjectViewModel(this, s)));
            }
        }
    }
}
