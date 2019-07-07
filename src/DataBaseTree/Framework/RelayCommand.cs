using System;
namespace DataBaseTree.Framework
{
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base((o) => execute(), (o) => canExecute?.Invoke() ?? true)
        {
        }
    }
}
