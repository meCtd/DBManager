using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace DBManager.Application.Behaviors
{
    public class TreeViewBehavior : Behavior<TreeView>
    {
        private bool _isSuspended;

        #region SelectedItem Property

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeViewBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behaviour = (TreeViewBehavior)sender;
            if (e.NewValue is TreeViewItem item && e.NewValue != null)
            {
                behaviour.PerformSuspension(() => item.SetValue(TreeViewItem.IsSelectedProperty, true));
            }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PerformSuspension(() => SelectedItem = e.NewValue);
        }

        private void PerformSuspension(Action action)
        {
            if (_isSuspended)
                return;
            try
            {
                _isSuspended = true;
                action?.Invoke();
            }
            finally
            {
                _isSuspended = false;
            }
        }
    }
}
