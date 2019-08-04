using System.Windows;

namespace DBManager.Application.Framework
{
    public static class WindowManager
    {
        public static void ShowWindow<TWindow,TContext>(TContext context) where TWindow : Window,new()
        {
            var window = new TWindow()
            {
                DataContext = context
            };

            window.Show();
        }
    }
}
