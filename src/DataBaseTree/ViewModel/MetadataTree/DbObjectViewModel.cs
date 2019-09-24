using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using DBManager.Default.Tree;

namespace DBManager.Application.ViewModel.MetadataTree
{
	public class DbObjectViewModel : MetadataViewModelBase
	{
		#region Properties

		public override DbObject Model { get; }

		public override string Name => Model.ToString();

		public override MetadataType Type => Model.Type;

		public override string Icon
		{
			get
			{
				switch (Type)
				{
					case MetadataType.Server:
						return "/Resources/Icons/Server.png";
					case MetadataType.Database:
						return "/Resources/Icons/Database.png";
					case MetadataType.Schema:
						return "/Resources/Icons/Schema.png";
					case MetadataType.Table:
						return "/Resources/Icons/Table.png";
					case MetadataType.View:
						return "/Resources/Icons/View.png";
					case MetadataType.Function:
						return "/Resources/Icons/Function.png";
					case MetadataType.Procedure:
						return "/Resources/Icons/Procedure.png";
					case MetadataType.Constraint:
						return "/Resources/Icons/Constraint.png";
					case MetadataType.Column:
						return "/Resources/Icons/Column.png";
					case MetadataType.Trigger:
						return "/Resources/Icons/Trigger.png";
					case MetadataType.Parameter:
						return "/Resources/Icons/Parameter.png";
					case MetadataType.Key:
						return "/Resources/Icons/Key.png";
					case MetadataType.Index:
						return "/Resources/Icons/Index.png";
					default:
						throw new ArgumentException();
				}
			}
		}

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
