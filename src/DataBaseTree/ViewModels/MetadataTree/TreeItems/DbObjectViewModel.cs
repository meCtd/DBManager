﻿using System.Threading;
using System.Threading.Tasks;

using DBManager.Application.ViewModels.General;
using DBManager.Default;
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

        public DbObjectViewModel(MetadataViewModelBase parent, DbObject model) : base(parent, AppContext.Current.Resolver.Get<IDialectComponent>().Hierarchy.Structure.GetValueOrDefault(model.Type)?.HasChildren ?? false)
        {
            Model = model;
        }

        protected override void RemoveChildrenFromModel()
        {
            Model.RemoveChildren();
        }

        protected override async Task LoadChildren()
        {
            var node = Context.Resolver.Get<IDialectComponent>().Hierarchy.Structure[Type];

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
