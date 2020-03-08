using System;
using System.Windows.Input;


namespace DBManager.Application.ViewModels.General
{
    public abstract class WindowViewModelBase : ViewModelBase, IWindowContext
    {
        private Action _closeAction;

        private ICommand _closeCommand;

        private string _header;

        public abstract string Header { get; }

        public virtual bool CanUserCloseWindow { get; } = false;

        public void Close()
        {
            Dispose();
            _closeAction.Invoke();
        }

        public ICommand CloseCommand { get; }


        void IWindowContext.SetCloseAction(Action closeAction)
        {
            _closeAction = closeAction;
        }
    }
}
