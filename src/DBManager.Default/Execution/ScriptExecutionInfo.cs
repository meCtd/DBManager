using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Default.Execution
{
    public class ScriptExecutionInfo : IScriptExecutionInfo
    {
        public IEnumerable<int> StatementAffectedRows { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
