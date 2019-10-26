using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using DBManager.Default.Tree;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
	public class DbObjectViewModel : MetadataViewModelBase
	{
		#region Properties

		public override DbObject Model { get; }

		public override string Name => Model.ToString();

		public override MetadataType Type => Model.Type;

		#endregion

		public DbObjectViewModel(MetadataViewModelBase parent, DbObject model) : base(parent, parent.Root.DbObjectLoader.Hierarchy.IsPossibleChilds(model.Type))
		{
			Model = model;
		}

		protected override async void LoadChildren()
		{
			IsBusy = false;
			Root.IsLoadingInProcess = false;
			try
			{
				
				IEnumerable<MetadataType> childs = Root.DbObjectLoader.Hierarchy.GetChildTypes(Type).ToArray();
				if (childs.Count() > 1)
				{
					foreach (var type in childs)
					{
						Children.Add(new CategoryViewModel(this, type));
					}
				}

				if (childs.Count() == 1)
				{
					if (Model.IsChildrenLoaded(childs.First()) == false)
					{
						if (!Root.IsConnected)
						{
							Root.RestoreConnection(false);
						}

						if (Root.IsConnected)
						{
							await Root.LoadModel(this,MetadataType.All);
						}
					}

					foreach (var child in Model.Children)
					{
						Children.Add(new DbObjectViewModel(this, child));
					}
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
	}
}
