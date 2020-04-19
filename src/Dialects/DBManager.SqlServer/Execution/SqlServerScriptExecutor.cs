using System;
using System.Data;
using System.Threading.Tasks;

using DBManager.Default.Execution;


namespace DBManager.SqlServer.Execution
{
    internal class SqlServerScriptExecutor : IScriptExecutor
    {
        private string _changeContextFormat = "USE {0};\n";

        public async Task<DataTable> ExecuteAsync(string sql, IExecutionContext context)
        {
            var dataTable = new DataTable();

            using (var connection = context.Connection.GetConnection())
            using (var command = SqlServerCreator.Instance.CreateCommand())
            {
                command.Connection = connection;
                await connection.OpenAsync(context.Token);
                
                command.CommandText = string.Concat(string.Format(_changeContextFormat, context.Context), sql);
                var reader = await command.ExecuteReaderAsync(context.Token);
                dataTable.Load(reader);
            }

            return dataTable;
        }
    }
}
