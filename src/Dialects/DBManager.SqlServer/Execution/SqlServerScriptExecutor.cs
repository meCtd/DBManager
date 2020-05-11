using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using DBManager.Default.Execution;
using Framework.Utils;

namespace DBManager.SqlServer.Execution
{
    internal class SqlServerScriptExecutor : IScriptExecutor
    {
        private const string ChangeContextFormat = "USE {0};\n";

        public async Task<IScriptExecutionResult> ExecuteAsync(string sql, IExecutionContext context)
        {
            var queryBuilder = new StringBuilder(string.Format(ChangeContextFormat, context.Context))
                .Append(sql);

            var composite = new CompositeDisposable();

            var connection = context.Connection.GetConnection();
            await connection.OpenAsync(context.Token);


            var command = SqlServerCreator.Instance.CreateCommand();
            command.Connection = connection;
            command.CommandText = queryBuilder.ToString();

            var sqlCommand = (SqlCommand)command;
            var affectedRows = new List<int>();
            sqlCommand.StatementCompleted += (s, e) => affectedRows.Add(e.RecordCount);

            var stopWatch = Stopwatch.StartNew();
            var reader = await command.ExecuteReaderAsync(context.Token);
            var elapsed = stopWatch.Elapsed;
            stopWatch.Stop();

            composite.Add(reader);
            composite.Add(command);
            composite.Add(connection);
            
            return new ScriptExecutionResult()
            {
                Info = new ScriptExecutionInfo()
                {
                    ExecutionTime = elapsed,
                    StatementAffectedRows = affectedRows
                },
                Reader = new DisposableToken<DbDataReader>(reader, s => { }, s => { composite.Dispose(); })
            };
        }
    }
}
