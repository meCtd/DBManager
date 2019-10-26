using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBManager.Default.DataBaseConnection;
using DBManager.Default.Tree;

namespace DBManager.Default.Loaders
{
    public class ObjectLoader : IObjectLoader
    {
        private readonly ConnectionData _connection;

        private readonly IDialectComponent _component;

        public bool IsOnline { get; set; }

        public ObjectLoader(IDialectComponent dialectComponent, ConnectionData connection)
        {
            _connection = connection;
            _component = dialectComponent;
        }

        public Task LoadChildrenAsync(DbObject obj)
        {
            return Task.Run(() =>
            {
                foreach (var child in _component.Hierarchy.Structure[obj.Type].ChildrenTypes)
                {
                    SetChildren(obj, child);
                }
            });
        }

        public Task LoadChildrenAsync(DbObject obj, MetadataType childType)
        {
            if (!_component.Hierarchy.Structure[obj.Type].ChildrenTypes.Contains(childType))
                throw new ArgumentException();

            return Task.Run(() => SetChildren(obj, childType));
        }

        public Task LoadPropertiesAsync(DbObject obj)
        {
            return Task.Run(() => SetObjectProperties(obj));
        }

        private void SetChildren(DbObject target, MetadataType childrenType)
        {
            using (var connection = _connection.GetConnection())
            {
                connection.Open();

                List<DbObject> children = new List<DbObject>();

                var command = _component.CreateCommand();
                command.Connection = connection;
                command.CommandText = _component.ScriptProvider.ProvideNameScript(target.Type, childrenType);


#warning MB DO NOT LOAD PROPERTIES HERE?
                foreach (var param in _component.ScriptProvider.GetChildrenLoadParameters(target, childrenType))
                {
                    command.Parameters.Add(param);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DbObject child = (MetadataTypeFactoryOld.ObjectCreator.Create(reader, childrenType));
                        if (!target.AddChild(child))
                        {
                            throw new ArgumentException();
                        }
                        children.Add(child);
                    }
                }

                foreach (var child in children)
                {
                    if (child.CanHaveDefinition)
                    {
                        command = _component.CreateCommand();
                        command.Connection = connection;
                        command.CommandText = _component.ScriptProvider.ProvideDefinitionScript();

                        foreach (var param in _component.ScriptProvider.GetDefinitionParameters(child))
                        {
                            command.Parameters.Add(param);
                        }

                        using (var descReader = command.ExecuteReader())
                        {

                            StringBuilder defText = new StringBuilder();
                            while (descReader.Read())
                            {
                                defText.Append(descReader.GetString(0));
                            }

                            child.Definition = defText.ToString();
                        }
                    }
                }

            }
        }

        private void SetObjectProperties(DbObject obj)
        {
            if (!(_connection.GetConnection() is SqlConnection))
                throw new InvalidCastException();
            using (SqlConnection connection = (SqlConnection)_connection.GetConnection())
            {
                connection.Open();

                string query = _component.ScriptProvider.ProvidePropertiesScript(obj);
                SqlCommand command = new SqlCommand(query, connection);

                foreach (var param in _component.ScriptProvider.GetLoadPropertiesParameters(obj))
                {
                    command.Parameters.Add(param);
                }

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.HasRows)
                        obj.Properties.Add(reader.GetName(i), reader.GetValue(i));
                }
            }

            obj.IsPropertyLoaded = true;
        }
    }
}
