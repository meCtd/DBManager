using System;
using DBManager.Default.Tree;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
	public abstract class MetadataViewModelBase : TreeViewItemViewModelBase
	{
        private bool _isBusy;

		public abstract MetadataType Type { get; }

		public abstract DbObject Model { get; }

		public virtual TreeRootViewModel Root { get; }

		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value); }
		}
        
		protected MetadataViewModelBase(MetadataViewModelBase parent, bool canBeChild) : base(parent, canBeChild)
		{
			Root = parent?.Root;
		}
		
		public virtual async void RefreshTreeItem()
		{
			
			if (Model.IsPropertyLoaded)
			{
				Model.DeleteProperties();
				await Root.LoadProperties(this);
			}
			if (HasFakeChild)
				return;
			Model.DeleteChildrens();
			Children.Clear();
			LoadChildren();
			OnTreeChanged(this, EventArgs.Empty);
		}
	}
}
