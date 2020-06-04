using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBManager.Default.Execution;

using Framework.Utils;

namespace DBManager.Access.Execution
{
    internal class AccessExecutor : IScriptExecutor
    {
        internal static AccessExecutor Instance { get; } = new AccessExecutor();

        private AccessExecutor()
        {
        }

        public bool HasContext => false;

        public async Task<IScriptExecutionResult> ExecuteAsync(string sql, IExecutionContext context)
        {
            var composite = new CompositeDisposable();

            var connection = context.Connection.GetConnection();
            await connection.OpenAsync(context.Token);


            var command = AccessCreator.Instance.CreateCommand();
            command.Connection = connection;
            command.CommandText = sql;

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
                    StatementAffectedRows = Enumerable.Empty<int>()
                },
                Reader = new DisposableToken<DbDataReader>(reader, s => { }, s => { composite.Dispose(); })
            };
        }
    }
}
