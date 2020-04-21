using System;
using System.Windows.Input;
using DBManager.Application.Utils;

namespace DBManager.Application.ViewModels.General
{
    public abstract class WindowViewModelBase : ViewModelBase, IWindowContext
    {
        private Action _closeAction;

        private ICommand _closeCommand;

        public abstract string Header { get; }

        public virtual bool CanUserCloseWindow { get; } = false;

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(s => Close()));

        public void Close()
        {
            Dispose();
            _closeAction.Invoke();
        }

        void IWindowContext.SetCloseAction(Action closeAction)
        {
            _closeAction = closeAction;
        }
}
}
