using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DBManager.Application.ViewModels.General
{
    public interface IWindowContext : INotifyPropertyChanged, IDisposable
    {
        string Header { get; }

        bool CanUserCloseWindow { get; }

        void SetCloseAction(Action closeAction);

        void Close();

        ICommand CloseCommand { get; }
    }
}
