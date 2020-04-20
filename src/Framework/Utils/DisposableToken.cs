using System;


namespace Framework.Utils
{
    /// <summary>
    /// A reusable disposable token that accepts initialization and uninitialization code.
    /// </summary>
    public class DisposableToken : DisposableToken<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableToken" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="initialize">The initialize action.</param>
        /// <param name="dispose">The dispose action.</param>
        /// <param name="tag">The tag.</param>
        public DisposableToken(object instance, Action<IDisposableToken<object>> initialize, Action<IDisposableToken<object>> dispose)
            : base(instance, initialize, dispose)
        {
        }
    }

    /// <summary>
    /// A reusable disposable token that accepts initialization and uninitialization code.
    /// </summary>
    public class DisposableToken<T> : IDisposableToken<T>
    {
        private Action<IDisposableToken<T>> _dispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableToken{T}" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="initialize">The initialize action that will be called with (token).</param>
        /// <param name="dispose">The dispose action that will be called with (instance, tag).</param>
        /// <param name="tag">The tag.</param>
        public DisposableToken(T instance, Action<IDisposableToken<T>> initialize, Action<IDisposableToken<T>> dispose)
        {
            Instance = instance;
            _dispose = dispose;

            initialize?.Invoke(this);
        }

        /// <summary>
        /// Gets the instance attached to this token.
        /// </summary>
        /// <value>The instance.</value>
        public T Instance { get; private set; }


        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _dispose?.Invoke(this);

            Instance = default(T);
            _dispose = null;
        }
    }
}

