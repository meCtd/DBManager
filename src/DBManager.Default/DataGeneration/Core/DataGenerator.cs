using System;
using System.Data;
using System.Data.SqlClient;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Default.DataGeneration.Core
{
    public class DataGenerator
    {
        private DataRecordCreator _recordCreator = new DataRecordCreator();

        public void GenerateData(IConnectionData connection, DataGenConfig configuration)
        {
            var table = CreateEmptyDataTable(configuration);

            GenerateRows(configuration, table);
            SaveTableToDataBase(table, connection, configuration.Scope.GetFirstAncestorOf<Database>().Name);
        }

        private DataTable CreateEmptyDataTable(DataGenConfig configuration)
        {
            var table = new DataTable(configuration.Scope.Name);

            foreach (var columnConfig in configuration.ColumnConfigurations.Keys)
            {
                table.Columns.Add(columnConfig);
            }

            return table;
        }

        private void GenerateRows(DataGenConfig configuration, DataTable table)
        {
            for (int i = 0; i < configuration.RowCount; i++)
            {
                var row = table.NewRow();
                _recordCreator.FillDataRecord(row, configuration);
                table.Rows.Add(row);
            }
        }

        private void SaveTableToDataBase(DataTable table, IConnectionData connection, string database)
        {
            using (var conn = connection.GetConnection())
            {
                conn.Open();

                conn.ChangeDatabase(database);

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)conn))
                {
                    foreach (DataColumn c in table.Columns)
                        bulkCopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);

                    bulkCopy.DestinationTableName = table.TableName;

                    bulkCopy.WriteToServer(table);

                }
            }
        }
    }
}
