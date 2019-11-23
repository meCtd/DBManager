using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using DBManager.Default;
using DBManager.Default.Tree;
using Ninject;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class DbObjectViewModel : MetadataViewModelBase
    {
        #region Properties

        public DbObject Model { get; }

        public override string Name => Model.ToString();

        public override MetadataType Type => Model.Type;

        #endregion

        public DbObjectViewModel(MetadataViewModelBase parent, DbObject model) : base(parent, Kernel.Get<IDialectComponent>().Hierarchy.Structure[model.Type].NeedCategory)
        {
            Model = model;
        }

        public override void RemoveChildren()
        {
            Model.RemoveChildren();
        }

        public override void LoadChildren()
        {
            throw new NotImplementedException();
        }
    }
}
