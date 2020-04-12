using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DBManager.Default;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.SqlServer.Printer
{
    //TODO USE WITH NORMALIZER and refactor class
    internal class SqlServerTablePrinter
    {
        private readonly StringBuilder _definition = new StringBuilder();

        private readonly Regex _bracketRegex = new Regex(@"(\,)[\n|\t]+\)", RegexOptions.Compiled);

        public string GetDefinition(DefinitionObject _object)
        {
            if (_object == null)
                throw new ArgumentNullException();
            _definition.Append($"CREATE TABLE [{_object.FullName.Schema}].[{_object.Name}](\n");

            SetColumns(_object);
            SetPrimaryUniqueKeys(_object, true);
            SetPrimaryUniqueKeys(_object, false);

            _definition.Append(")\n\n");

            SetCheckConstraints(_object);
            _definition.Append("\n\n");

            SetForeingKeyConstraint(_object);

            string definition = _definition.ToString();
            Match match;
            while ((match = _bracketRegex.Match(definition)).Success)
            {
                definition = definition.Remove(match.Groups[1].Index, 1);
            }
            return definition;

        }

        private void SetColumns(DbObject _object)
        {
            foreach (var column in _object.Children.OfType<Column>())
            {
                _definition.Append($"\t[{column.Name}] {column.DbType.Name} ");
                if ((bool)column.Properties[Constants.IsIdentityProperty])
                {
                    _definition.Append(
                        $"IDENTITY ({column.Properties[Constants.SeedValueProperty]},{column.Properties[Constants.SeedIncrementProperty]}) ");
                }
                 
                if (!string.IsNullOrWhiteSpace(column.Properties[Constants.DefaultValueProperty].ToString()))
                {
                    _definition.Append($"CONSTRAINT [DF_{column.FullName.Schema}_{column.Name}] DEFAULT {column.Properties[Constants.DefaultValueProperty]} ,\n");
                }
                else
                {
                    if ((bool)column.Properties[Constants.IsNullableProperty])
                    {
                        _definition.Append("NULL ,\n");
                    }
                    else
                        _definition.Append("NOT NULL ,\n");
                }
            }
        }

        private void SetPrimaryUniqueKeys(DbObject _object, bool keyType)
        {
            string search = keyType ? "PRIMARY" : "UNIQUE";
            string name = keyType ? "PRIMARY KEY" : "UNIQUE";


            IEnumerable<DbObject> matches = _object.Children.Where(key => key.Type == MetadataType.Key && key.Properties[Constants.TypeProperty].ToString().Contains(search));
            foreach (var match in matches)
            {
                _definition.Append($"CONSTRAINT [{match.Name}] {name} \n(\n");
                IEnumerable<string> columns = match.Properties[Constants.ColumnsProperty].ToString().Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s));
                foreach (var column in columns)
                {
                    _definition.Append($"\t[{column}] ,\n");
                }

                _definition.Append(") ,\n");
            }

        }

        private void SetForeingKeyConstraint(DbObject _object)
        {
            IEnumerable<DbObject> foreingnKeys = _object.Children.Where(child => child.Type == MetadataType.Key && child.Properties[Constants.TypeProperty].ToString().Contains("FOREIGN"));
            foreach (var key in foreingnKeys)
            {
                _definition.Append(
                    $"ALTER TABLE [{_object.FullName.Schema}].[{_object.Name}]  WITH CHECK ADD CONSTRAINT [{key.Name}] FOREIGN KEY([{key.Properties[Constants.ColumnsProperty]}]) REFERENCES [{key.Properties[Constants.ReferenceSchemaNameProperty]}].[{key.Properties[Constants.ReferenceTableNameProperty]}] ([{key.Properties[Constants.ReferenceColumnProperty]}])\n");
            }
        }

        private void SetCheckConstraints(DbObject _object)
        {
            IEnumerable<DbObject> constraints = _object.Children.Where(child => child.Type == MetadataType.Constraint);
            foreach (var constraint in constraints)
            {
                _definition.Append(
                    $"ALTER TABLE [{_object.FullName.Schema}].[{_object.Name}]  WITH CHECK ADD CONSTRAINT [{constraint.Name}] CHECK {constraint.Properties[Constants.DefinitionProperty]} \n");
            }
        }
    }
}
