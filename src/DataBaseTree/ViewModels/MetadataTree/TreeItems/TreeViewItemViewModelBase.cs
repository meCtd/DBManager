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

        public event EventHandler<ValueChangedEventArgs<bool>> ExpandChanged;

        public event EventHandler Refreshed;

        public TreeViewItemViewModelBase Parent { get; }

        public ObservableCollection<TreeViewItemViewModelBase> Children { get; }

        private bool HasDummyChild => Children.Count == 1 && Children[0] is DummyChild;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (!_canHaveChildren)
                    return;

                var oldValue = _isExpanded; 

                if (SetProperty(ref _isExpanded, value))
                {
                    if (value)
                        ExpandParent();

                    ExpandChanged?.Invoke(this, new ValueChangedEventArgs<bool>(oldValue, value));
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (SetProperty(ref _isSelected, value) && value)
                    ExpandParent();
            }
        }

        protected TreeViewItemViewModelBase(TreeViewItemViewModelBase parent, bool canHaveChildren = true)
        {
            Parent = parent;
            Children = new ObservableCollection<TreeViewItemViewModelBase>();

            _canHaveChildren = canHaveChildren;

            Refreshed += (s, e) => Setup();

            ExpandChanged += (s, e) =>
            {
                if (e.New && HasDummyChild)
                    Children.Remove(_dummyChild);
            };

            Setup();
        }

        private void ExpandParent()
        {
            if (!ReferenceEquals(Parent, null) && Parent.IsExpanded)
                Parent.IsExpanded = true;
        }

        protected void Refresh()
        {
            Children.Clear();
            _isExpanded = false;

            Refreshed?.Invoke(this, EventArgs.Empty);

            OnPropertyChanged(nameof(IsExpanded));
        }

        private void Setup()
        {
            if (_canHaveChildren)
                Children.Add(_dummyChild);
        }

        private sealed class DummyChild : TreeViewItemViewModelBase
        {
            public DummyChild() : base(null)
            {
            }
        }
    }
}