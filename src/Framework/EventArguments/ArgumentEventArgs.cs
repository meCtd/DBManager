using System;


namespace Framework.EventArguments
{
    public class ArgumentEventArgs<T> : EventArgs
    {
        public T Argument { get; }

        public ArgumentEventArgs(T argument)
        {
            Argument = argument;
        }
    }
}
