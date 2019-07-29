using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Windows;
using DBManager.Application.View;
using DBManager.Application.ViewModel.ConnectionViewModel;
using DBManager.Default.Loaders;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Application.ViewModel.TreeViewModel
{
	public class TreeRootViewModel : MetadataViewModelBase
	{
		#region Fields

		private bool _isLoadingInProcess;

		private bool _isConnected;

		#endregion

		public bool IsConnected
		{
			get { return _isConnected; }
			set { SetProperty(ref _isConnected, value); }
		}

		public override DbObject Model { get; }

		public override DbEntityType Type => DbEntityType.Server;

		public override string Icon => "/Resources/Icons/Server.png";

		public ObjectLoader DbObjectLoader { get; }

		public override TreeRootViewModel Root => this;

		public bool IsLoadingInProcess
		{
			get { return _isLoadingInProcess; }
			set { SetProperty(ref _isLoadingInProcess, value); }
		}

		public bool IsDefaultDatabase { get; }

		public override string Name => Model.Name;

		public TreeRootViewModel(ObjectLoader objectLoader) : base(null, true)
		{
			DbObjectLoader = objectLoader;
			Model = new Server(DbObjectLoader.Connection.Server);
			IsDefaultDatabase = string.IsNullOrWhiteSpace(DbObjectLoader.Connection.InitialCatalog);
			IsConnected = true;
		}

		public TreeRootViewModel(DbObject model, ObjectLoader objectLoader) : base(null, true)
		{
			DbObjectLoader = objectLoader;
			Model = model;
			IsDefaultDatabase = string.IsNullOrWhiteSpace(DbObjectLoader.Connection.InitialCatalog);
			IsConnected = false;

		}

		protected override async void LoadChildren()
		{
			IsBusy = true;
			IsLoadingInProcess = true;
			try
			{
				if (!IsDefaultDatabase)
				{
					Database db = new Database(DbObjectLoader.Connection.InitialCatalog);
					Model.AddChild(db);
					Children.Add(new DbObjectViewModel(this, db));

					return;
				}

				if (Model.IsChildrenLoaded(DbEntityType.Database) == false)
				{
					if (!IsConnected)
						RestoreConnection(false);
					if (IsConnected)
						await LoadModel(this, DbEntityType.Database);
				}

				foreach (var child in Model.Children)
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
				IsLoadingInProcess = false;
			}
		}

		public void RestoreConnection(bool check)
		{
			if (!check)
				if (MessageBox.Show("You're not connected. Are you want to restore connection?", "Connection",
						MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
				{
					return;
				}

			ConnectionWindow window = new ConnectionWindow();
			ConnectionWindowViewModel data = (ConnectionWindowViewModel)window.DataContext;
			data.SelectedBaseType = DbObjectLoader.Connection.Type;

			ConnectionViewModelBase conenctData = data.ConnectionData;
			conenctData.CanChange = false;
			conenctData.Server = DbObjectLoader.Connection.Server;
			conenctData.Port = DbObjectLoader.Connection.Port;
			conenctData.UserId = DbObjectLoader.Connection.UserId;

			switch (conenctData)
			{
				case MsSqlConnectionViewModel msSql:
					msSql.IntegratedSecurity = ((MsSqlServer)DbObjectLoader.Connection).IntegratedSecurity;
					msSql.Pooling = ((MsSqlServer)DbObjectLoader.Connection).Pooling;
					msSql.ConnectionTimeout = ((MsSqlServer)DbObjectLoader.Connection).ConnectionTimeout;
					break;
			}

			if (window.ShowDialog() == true)
			{
				IsConnected = true;
				Root.DbObjectLoader.Connection = data.ConnectionData.Connection;
			}
		}

		public async Task LoadModel(MetadataViewModelBase obj, DbEntityType type)
		{
			Root.DbObjectLoader.Connection.InitialCatalog = obj.Model.DataBaseName;
			try
			{
				if (Root.IsConnected)
				{
					if (type == DbEntityType.All)
						await DbObjectLoader.LoadChildrenAsync(obj.Model);
					else
						await DbObjectLoader.LoadChildrenAsync(obj.Model, type);
				}
			}
			catch (DbException d)
			{
				MessageBox.Show(d.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				if (Root.IsDefaultDatabase)
					Root.DbObjectLoader.Connection.InitialCatalog = string.Empty;
			}

		}

		public async Task LoadProperties(MetadataViewModelBase obj)
		{
			obj.IsBusy = true;
			Root.IsLoadingInProcess = true;
			try
			{
				Root.DbObjectLoader.Connection.InitialCatalog = obj.Model.DataBaseName;
				await Root.DbObjectLoader.LoadPropertiesAsync(obj.Model);

			}
			catch (DbException e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			finally
			{
				obj.IsBusy = false;
				Root.IsLoadingInProcess = false;
				if (Root.IsDefaultDatabase)
					Root.DbObjectLoader.Connection.InitialCatalog = string.Empty;
			}
		}

		public void RefreshProperties(MetadataViewModelBase obj)
		{
			obj.Model.DeleteProperties();
			obj.Model.IsPropertyLoaded = false;
		}

	}

}
