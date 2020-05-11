using System.Data.Common;
using System.Threading.Tasks;
using Framework.Utils;

namespace DBManager.Default.Execution
{
    public class ScriptExecutionResult : IScriptExecutionResult
    {
        public IScriptExecutionInfo Info { get; set; }
        public IDisposableToken<DbDataReader> Reader { get; set; }
    }
}
