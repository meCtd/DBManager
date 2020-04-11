using System.Windows;

namespace DBManager.Application.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для TreeWindow.xaml
    /// </summary>
    public partial class TreeWindow : Window
    {
        public TreeWindow()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { WindowState = WindowState.Maximized; });
        }

        private void RestoreDownClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { WindowState = WindowState.Normal; });
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { WindowState = WindowState.Minimized; });
        }
    }
}
