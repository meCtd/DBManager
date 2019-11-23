using DBManager.Default.Loaders;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
	public class ServerViewModel : MetadataViewModelBase
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
        

		public bool IsDefaultDatabase { get; }


		public ServerViewModel(ObjectLoader objectLoader) : base(null, true)
		{
			DbObjectLoader = objectLoader;
			Model = new Server(DbObjectLoader.Connection.Server);
			IsDefaultDatabase = string.IsNullOrWhiteSpace(DbObjectLoader.Connection.InitialCatalog);
			IsConnected = true;
		}

		public ServerViewModel(DbObject model, ObjectLoader objectLoader) : base(null, true)
		{
			DbObjectLoader = objectLoader;
			Model = model;
			IsDefaultDatabase = string.IsNullOrWhiteSpace(DbObjectLoader.Connection.InitialCatalog);
			IsConnected = false;

		}
		

	}

}
