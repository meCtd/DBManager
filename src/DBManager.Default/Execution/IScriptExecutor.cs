using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Framework.Utils;

namespace DBManager.Default.Execution
{
    public interface IScriptExecutor
    {
        bool HasContext { get; }
        Task<IScriptExecutionResult> ExecuteAsync(string sql, IExecutionContext context);
    }
}
