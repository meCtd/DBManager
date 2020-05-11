using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Framework.Utils;

namespace DBManager.Default.Execution
{
    public interface IScriptExecutor
    {
        Task<IScriptExecutionResult> ExecuteAsync(string sql, IExecutionContext context);
    }
}
