using System;
using System.Data.Common;
using System.Linq;
using System.Windows;
using DBManager.Default.Tree;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class CategoryViewModel : MetadataViewModelBase
    {
        public override DbObject Model { get; }

        public override string Name
        {
            get
            {
                switch (Type)
                {
                    case MetadataType.Table:
                        return "Tables";
                    case MetadataType.View:
                        return "Views";
                    case MetadataType.Function:
                        return "Functions";
                    case MetadataType.Procedure:
                        return "Procedures";
                    case MetadataType.Constraint:
                        return "Constraints";
                    case MetadataType.Column:
                        return "Columns";
                    case MetadataType.Trigger:
                        return "Triggers";
                    case MetadataType.Parameter:
                        return "Parameters";
                    case MetadataType.Key:
                        return "Keys";
                    case MetadataType.Index:
                        return "Indexes";
                    case MetadataType.Schema:
                        return "Schemas";

                    default:
                        throw new ArgumentException();

                }
            }
        }

        public override MetadataType Type { get; }

        public CategoryViewModel(MetadataViewModelBase parent, MetadataType type) : base(parent, true)
        {
            Type = type;
            Model = parent.Model;
        }

        protected async void LoadChildren()
        {
            IsBusy = true;
            Root.IsLoadingInProcess = true;
            try
            {
                //    if (Model.IsChildrenLoaded(Type) == false)
                //    {
                //        if (!Root.IsConnected)
                //        {
                //            Root.RestoreConnection(false);
                //        }

                //        if (Root.IsConnected)
                //        {
                //            await Root.LoadModel(this, Type);
                //        }
                //    }

                foreach (var child in Model.Children.Where(s => s.Type == Type))
                {
                    Children.Add(new DbObjectViewModel(this, child));
                }
            }
            catch (DbException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
                Root.IsLoadingInProcess = false;
                OnTreeChanged(this, EventArgs.Empty);
            }
        }

        public override void RefreshTreeItem()
        {
            Model.DeleteChildren(Type);
            Children.Clear();


            LoadChildren();
            OnTreeChanged(this, EventArgs.Empty);
        }
    }
}
