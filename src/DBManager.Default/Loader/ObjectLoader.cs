using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Tree;

namespace DBManager.Default.Loader
{
    public class ObjectLoader : IObjectLoader
    {
        private IConnectionData _connection { get; }

        private readonly IDialectComponent _component;

        public ObjectLoader(IDialectComponent dialectComponent, IConnectionData connection)
        {
            _connection = connection;
            _component = dialectComponent;
        }

        public Task LoadServerProperties(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public async Task LoadChildrenAsync(DbObject obj, CancellationToken token)
        {
            foreach (var child in _component.Hierarchy.Structure[obj.Type].ChildrenTypes)
            {
                await LoadChildrenAsync(obj, child, token);
            }
        }

        public Task LoadChildrenAsync(DbObject obj, MetadataType childType, CancellationToken token)
        {
            if (!_component.Hierarchy.Structure[obj.Type].ChildrenTypes.Contains(childType))
                throw new ArgumentException();

            return LoadChildrenInternal(obj, childType, token);
        }

        private async Task LoadChildrenInternal(DbObject target, MetadataType childrenType, CancellationToken token)
        {
            using (var connection = _connection.GetConnection())
            {
                await connection.OpenAsync(token);

                var command = _component.CreateCommand();
                command.Connection = connection;
                command.CommandText = _component.ScriptProvider.ProvideNameScript(target, childrenType);

                using (var reader = await command.ExecuteReaderAsync(token))
                {
                    while (reader.Read())
                    {
                        target.AddChild(_component.ObjectFactory.Create(reader, childrenType));
                    }
                }
            }
        }

        public async Task LoadPropertiesAsync(DbObject obj, CancellationToken token)
        {
            using (var connection = _connection.GetConnection())
            {
                await connection.OpenAsync(token);

                string query = _component.ScriptProvider.ProvidePropertiesScript(obj);

                var command = _component.CreateCommand();
                command.Connection = connection;
                command.CommandText = query;

                using (var reader = await command.ExecuteReaderAsync(token))
                {
                    reader.Read();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.HasRows)
                            obj.Properties.Add(reader.GetName(i), reader.GetValue(i));
                    }
                }

            }
        }
    }
}
