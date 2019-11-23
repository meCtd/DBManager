using System;

namespace Framework.EventArguments
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T Old { get; }
        public T New { get; }

        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            Old = oldValue;
            New = newValue;
        }
    }
}
