using System;

namespace Framework.Utils
{
    public interface IDisposableToken<T> : IDisposable
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        T Instance { get; }
    }
}