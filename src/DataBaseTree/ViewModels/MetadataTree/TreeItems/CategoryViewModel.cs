using System;
using System.Data.Common;
using System.Linq;
using System.Windows;
using DBManager.Default.Tree;
using Plurally;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class CategoryViewModel : MetadataViewModelBase
    {
        private static readonly Pluralizer _pluralizer = new Pluralizer();

        public override string Name { get; }

        public override MetadataType Type { get; }

        public CategoryViewModel(MetadataViewModelBase parent, MetadataType type) : base(parent, true)
        {
            Type = type;
            Name = _pluralizer.Pluralize(type.ToString());
        }

        public override void RemoveChildren()
        {
            ObjectParent.Model.RemoveChildren(Type);
        }

        public override void LoadChildren()
        {
            throw new NotImplementedException();
        }
    }
}
