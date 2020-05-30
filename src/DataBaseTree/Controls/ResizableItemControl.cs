using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DBManager.Application.Controls
{
    [TemplatePart(Name = "PART_Grid", Type = typeof(Grid))]
    public class ResizableItemControl : ItemsControl
    {
        private Grid _root;

        static ResizableItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizableItemControl), new FrameworkPropertyMetadata(typeof(ResizableItemControl)));
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(ResizableItemControl), new PropertyMetadata(default(Orientation), (o, args) => ((ResizableItemControl)o).Setup()));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty ElementSizeProperty = DependencyProperty.Register(
            "ElementSize", typeof(double), typeof(ResizableItemControl), new PropertyMetadata(200d));

        public double ElementSize
        {
            get { return (double)GetValue(ElementSizeProperty); }
            set { SetValue(ElementSizeProperty, value); }
        }

        public static readonly DependencyProperty SplitterSizeProperty = DependencyProperty.Register(
            "SplitterSize", typeof(double), typeof(ResizableItemControl), new PropertyMetadata(5d));

        public double SplitterSize
        {
            get { return (double)GetValue(SplitterSizeProperty); }
            set { SetValue(SplitterSizeProperty, value); }
        }

        public static readonly DependencyProperty MinElementSizeProperty = DependencyProperty.Register(
            "MinElementSize", typeof(double), typeof(ResizableItemControl), new PropertyMetadata(50d));

        public double MinElementSize
        {
            get { return (double)GetValue(MinElementSizeProperty); }
            set { SetValue(MinElementSizeProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template != null)
                _root = Template.FindName("PART_Grid", this) as Grid;

            Setup();
        }

        /// <summary>
        /// Called when Items in ItemsCollection changing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    Setup();
                    break;
            }
        }

        private void Setup()
        {
            if (_root is null)
                return;

            _root.Children.Clear();
            _root.ColumnDefinitions.Clear();
            _root.RowDefinitions.Clear();

            var items = ItemsSource?.Cast<object>().ToArray() ?? Array.Empty<object>();

            for (int i = 0; i < items.Length; i++)
            {
                var visualItem = (FrameworkElement)ItemTemplate.LoadContent();
                visualItem.DataContext = items[i];

                if (i != Items.Count - 1)
                    AddObject(visualItem, new GridLength(ElementSize, GridUnitType.Pixel), MinElementSize);
                else
                    //AddObject(visualItem, new GridLength(1, GridUnitType.Star));
                    AddObject(visualItem, new GridLength(ElementSize, GridUnitType.Pixel), MinElementSize);


                if (i != Items.Count - 1)
                    AddObject(CreateSplitter(), GridLength.Auto);
            }
        }

        void AddObject(UIElement element, GridLength size, double minSize = 0)
        {
            if (Orientation == Orientation.Horizontal)
            {
                _root.ColumnDefinitions.Add(new ColumnDefinition() { Width = size, MinWidth = minSize });
                Grid.SetColumn(element, _root.ColumnDefinitions.Count - 1);
                _root.Children.Add(element);

            }
            else
            {
                _root.RowDefinitions.Add(new RowDefinition() { Height = size, MinHeight = minSize });
                Grid.SetRow(element, _root.RowDefinitions.Count - 1);
                _root.Children.Add(element);
            }
        }

        GridSplitter CreateSplitter()
        {
            var splitter = new GridSplitter
            {
                HorizontalAlignment = Orientation == Orientation.Horizontal
                    ? HorizontalAlignment.Center
                    : HorizontalAlignment.Stretch,

                VerticalAlignment = Orientation == Orientation.Vertical
                    ? VerticalAlignment.Center
                    : VerticalAlignment.Stretch,
                ResizeBehavior = GridResizeBehavior.PreviousAndCurrent,
                Background = new SolidColorBrush(Colors.Transparent)

            };

            if (Orientation == Orientation.Horizontal)
                splitter.Width = SplitterSize;
            else
                splitter.Height = SplitterSize;

            return splitter;
        }
    }
}
