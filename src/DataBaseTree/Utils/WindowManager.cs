using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using DBManager.Application.View.Windows;
using DBManager.Application.ViewModels.General;


namespace DBManager.Application.Utils
{
    interface IWindowManager : INotifyPropertyChanged
    {
        Window CurrentWindow { get; }

        void ShowWindow<T>(T windowContext) where T : IWindowContext;

        void RunOnUi(Action action);
    }

    class WindowManager : ViewModelBase, IWindowManager
    {
        private readonly Stack<Window> _windows = new Stack<Window>(new[] { App.Current.MainWindow });

        public Window CurrentWindow => _windows.Peek();

        public void ShowWindow<T>(T windowContext) where T : IWindowContext
        {
            var window = new ContentWindow();

            windowContext.SetCloseAction(() => RunOnUi(window.Close));
            window.DataContext = windowContext;
            window.Owner = CurrentWindow;


            _windows.Push(window);
            OnPropertyChanged(nameof(CurrentWindow));

            window.ShowDialog();

            _windows.Pop();
            OnPropertyChanged(nameof(CurrentWindow));
        }

        public void RunOnUi(Action action)
        {
            if (App.Current.Dispatcher.CheckAccess())
                action?.Invoke();

            App.Current.Dispatcher.Invoke(action);
        }
    }
}
