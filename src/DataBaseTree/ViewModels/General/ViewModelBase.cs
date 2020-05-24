using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DBManager.Application.ViewModels.General
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected AppContext Context => AppContext.Current;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, Action<T, T> onChanged = null, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            var oldValue = storage;
            storage = value;
            onChanged?.Invoke(oldValue, storage);
            OnPropertyChanged(propertyName);

            return true;
        }

        protected void RefreshAllBindings()
        {
            OnPropertyChanged(string.Empty);
        }

        public virtual void Dispose()
        {
            PropertyChanged = null;
        }
    }
}
