using System;
using System.Collections.ObjectModel;

using DBManager.Application.ViewModels.General;

using Framework.EventArguments;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public abstract class TreeViewItemViewModelBase : ViewModelBase
    {
        private static readonly TreeViewItemViewModelBase _dummyChild = new DummyChild();

        private readonly bool _canHaveChildren;

        private bool _isExpanded;

        private bool _isSelected;

        public ObservableCollection<TreeViewItemViewModelBase> Children { get; }

        public TreeViewItemViewModelBase Parent { get; }
        
        public event EventHandler<ValueChangedEventArgs<bool>> ExpandChanged;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if(!_canHaveChildren)
                    return;
                
                var oldValue = _isExpanded;
                if (SetProperty(ref _isExpanded, value))
                    ExpandChanged?.Invoke(this, new ValueChangedEventArgs<bool>(oldValue, value));
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        protected TreeViewItemViewModelBase(TreeViewItemViewModelBase parent, bool canHaveChildren = true)
        {
            Parent = parent;
            Children = new ObservableCollection<TreeViewItemViewModelBase>();

            _canHaveChildren = canHaveChildren;

            if (canHaveChildren)
                Children.Add(_dummyChild);

            ExpandChanged += OnExpandChanged;
        }

        private void OnExpandChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            Children.Remove(_dummyChild);
            ExpandChanged -= OnExpandChanged;
        }

        private sealed class DummyChild : TreeViewItemViewModelBase
        {
            public DummyChild() : base(null)
            {
            }
        }
    }
}