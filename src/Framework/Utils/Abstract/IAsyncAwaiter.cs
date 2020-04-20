using System;
using System.Threading.Tasks;

namespace Framework.Utils
{
    public interface IAsyncAwaiter
    {
        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task, returning it's value
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside of the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of tasks that can access this task before it waiting</param>
        /// <returns></returns>
        Task<T> AwaitResultAsync<T>(string key, Func<Task<T>> task, int maxAccessCount = 1);

        /// <summary>
        /// Awaits for any outstanding tasks to complete that are accessing the same key then runs the given task
        /// </summary>
        /// <param name="key">The key to await</param>
        /// <param name="task">The task to perform inside of the semaphore lock</param>
        /// <param name="maxAccessCount">If this is the first call, sets the maximum number of tasks that can access this task before it waiting</param>
        /// <returns></returns>
        Task AwaitAsync(string key, Func<Task> task, int maxAccessCount = 1);
    }
}