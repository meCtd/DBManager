using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Default.Execution
{
    public interface IScriptExecutionInfo
    {
        IEnumerable<int> StatementAffectedRows { get; }
        TimeSpan ExecutionTime { get; }
    }
}
