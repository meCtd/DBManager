using System;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public abstract class TreeViewItemViewModelBase : BindableBase
    {
        #region Fields

        private static readonly TreeViewItemViewModelBase _dummyChild = new FakeChild();

        private bool _isExpanded;

        private bool _isSelected;

        #endregion

        #region Properties

        public event EventHandler TreeChanged;

        public ObservableCollection<TreeViewItemViewModelBase> Children { get; }

        public TreeViewItemViewModelBase Parent { get; }


        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (SetProperty(ref _isExpanded, value) && value)
                    LoadChildrenInternal();

            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public abstract string Name { get; }

        protected void OnTreeChanged(object sender, EventArgs e)
        {
            if (Parent != null)
                Parent.OnTreeChanged(sender, e);
            else
            {
                TreeChanged?.Invoke(sender, e);
            }
        }

        #endregion


        protected TreeViewItemViewModelBase(TreeViewItemViewModelBase parent, bool canBeChild)
        {
            Parent = parent;
            Children = new ObservableCollection<TreeViewItemViewModelBase>();
            if (canBeChild)
                Children.Add(_dummyChild);
        }


        private void LoadChildrenInternal()
        {
            if (Children.Count == 1 && ReferenceEquals(Children.First(), _dummyChild))
                Children.Clear();

            //LoadChildren();
        }

        private void Refresh()
        {

        }

        //protected abstract void Refresh()
        //{

        //}


        //protected abstract void LoadChildren();




        private sealed class FakeChild : TreeViewItemViewModelBase
        {
            public override string Name => string.Empty;

            public FakeChild() : base(null, true)
            {

            }
        }
    }
}










