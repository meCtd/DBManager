using System.Data;

using System.Threading.Tasks;


namespace DBManager.Default.Execution
{
    public interface IScriptExecutor
    {
        Task<DataTable> ExecuteAsync(string sql, IExecutionContext context);
    }
}
