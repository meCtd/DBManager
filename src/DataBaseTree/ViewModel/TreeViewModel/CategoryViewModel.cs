using System;
using System.Data.Common;
using System.Linq;
using System.Windows;
using DataBaseTree.Model.Tree;

namespace DataBaseTree.ViewModel.TreeViewModel
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
					case DbEntityType.Table:
						return "Tables";
					case DbEntityType.View:
						return "Views";
					case DbEntityType.Function:
						return "Functions";
					case DbEntityType.Procedure:
						return "Procedures";
					case DbEntityType.Constraint:
						return "Constraints";
					case DbEntityType.Column:
						return "Columns";
					case DbEntityType.Trigger:
						return "Triggers";
					case DbEntityType.Parameter:
						return "Parameters";
					case DbEntityType.Key:
						return "Keys";
					case DbEntityType.Index:
						return "Indexes";
					case DbEntityType.Schema:
						return "Schemas";

					default:
						throw new ArgumentException();

				}
			}
		}

		public override DbEntityType Type { get; }

		public override string Icon => @"/Resources/Icons/Category.png";

		public CategoryViewModel(MetadataViewModelBase parent, DbEntityType type) : base(parent, true)
		{
			Type = type;
			Model = parent.Model;
		}

		protected  override async void LoadChildren()
		{
			IsBusy = true;
			Root.IsLoadingInProcess = true;
			try
			{
				if (Model.IsChildrenLoaded(Type) == false)
				{
					if (!Root.IsConnected)
					{
						Root.RestoreConnection(false);
					}

					if (Root.IsConnected)
					{
						await Root.LoadModel(this,Type);
					}
				}

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
			if (HasFakeChild)
				return;
			Model.DeleteChildrens(Type);
			Children.Clear();
			LoadChildren();
			OnTreeChanged(this, EventArgs.Empty);
		}
	}
}
