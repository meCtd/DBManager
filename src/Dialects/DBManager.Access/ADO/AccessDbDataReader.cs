using System;
using System.Collections;
using System.Data.Common;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Access.Dao;

namespace DBManager.Access.ADO
{
    class AccessDbDataReader : DbDataReader
    {
        private bool _isDisposed;

        private bool _isFirstRead = true;
        private Recordset _executeResult;

        private AccessDbDataReader(int recordsAffected)
        {
            RecordsAffected = recordsAffected;
        }

        private AccessDbDataReader(Recordset result)
        {
            _executeResult = result;
            RecordsAffected = -1;
        }

        public static AccessDbDataReader CreateDbReader(AccessDbConnection connection, string commandText)
        {
            if (commandText.Trim().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                return new AccessDbDataReader(connection.DaoDatabase.OpenRecordset(commandText));

            var affected = ExecuteActionQuery(connection, commandText);
            return new AccessDbDataReader(affected);
        }

        private static int ExecuteActionQuery(AccessDbConnection connection, string commandText)
        {
            var statement = commandText.Trim().Split(' ')[0].ToUpper();

            switch (statement)
            {
                case "INSERT":
                case "UPDATE":
                case "DELETE":
                    connection.DaoDatabase.Execute(commandText);
                    return connection.DaoDatabase.RecordsAffected;
                default:
                    connection.DaoDatabase.Execute(commandText);
                    return -1;
            }
        }

        public override object this[int ordinal] => _executeResult.Fields[ordinal].Value;
        public override object this[string name] => _executeResult.Fields[name].Value;

        public override int Depth => 0;
        public override int FieldCount => _executeResult == null ? 0 : _executeResult.Fields.Count;
        public override bool HasRows => _executeResult?.RecordCount > 0;
        public override bool IsClosed => _isDisposed;
        public override int RecordsAffected { get; }

        public override bool GetBoolean(int ordinal)
        {
            return (bool)_executeResult.Fields[ordinal].Value;
        }

        public override byte GetByte(int ordinal)
        {
            return (byte)_executeResult.Fields[ordinal].Value;
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            return (char)_executeResult.Fields[ordinal].Value;
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            return ((DataTypeEnum)_executeResult.Fields[ordinal].Type).ToString().ToUpper();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return (DateTime)_executeResult.Fields[ordinal].Value;
        }

        public override decimal GetDecimal(int ordinal)
        {
            return (decimal)_executeResult.Fields[ordinal].Value;
        }

        public override double GetDouble(int ordinal)
        {
            return (double)_executeResult.Fields[ordinal].Value;
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            return _executeResult.Fields[ordinal].Value.GetType();
        }

        public override float GetFloat(int ordinal)
        {
            return (float)_executeResult.Fields[ordinal].Value;
        }

        public override Guid GetGuid(int ordinal)
        {
            return (Guid)_executeResult.Fields[ordinal].Value;
        }

        public override short GetInt16(int ordinal)
        {
            return (short)_executeResult.Fields[ordinal].Value;
        }

        public override int GetInt32(int ordinal)
        {
            return (int)_executeResult.Fields[ordinal].Value;
        }

        public override long GetInt64(int ordinal)
        {
            return (long)_executeResult.Fields[ordinal].Value;
        }

        public override string GetName(int ordinal)
        {
            return _executeResult.Fields[ordinal].Name;
        }

        public override int GetOrdinal(string name)
        {
            return _executeResult.Fields[name].OrdinalPosition;
        }

        public override string GetString(int ordinal)
        {
            return (string)_executeResult.Fields[ordinal].Value;
        }

        public override object GetValue(int ordinal)
        {
            return _executeResult.Fields[ordinal].Value;
        }

        public override int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public override bool IsDBNull(int ordinal)
        {
            return _executeResult.Fields[ordinal].Value == null;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override bool Read()
        {
            if (_isFirstRead)
                _isFirstRead = false;
            else if (!_executeResult.EOF)
                _executeResult.MoveNext();

            return !_executeResult.EOF;
        }

        public override void Close()
        {
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed && _executeResult != null)
            {
                _executeResult.Close();
                Marshal.ReleaseComObject(_executeResult);
                _executeResult = null;
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
