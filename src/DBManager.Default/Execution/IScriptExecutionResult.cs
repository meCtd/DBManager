using System.Data.Common;
using Framework.Utils;

namespace DBManager.Default.Execution
{
    public interface IScriptExecutionResult
    {
        IScriptExecutionInfo Info { get; }
        IDisposableToken<DbDataReader> Reader { get; }
    }
}
