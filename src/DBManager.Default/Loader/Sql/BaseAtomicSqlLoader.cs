using System.Data.Common;
using System.Threading.Tasks;

using DBManager.Default.MetadataFactory;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;


namespace DBManager.Default.Loader.Sql
{
    public abstract class BaseAtomicSqlLoader : IAtomicLoader
    {
        public const string Name = "Name";

        protected readonly IDialectComponent _components;

        public abstract MetadataType Type { get; }

        protected BaseAtomicSqlLoader(IDialectComponent components)
        {
            _components = components;
        }

        public async Task LoadChildren(ILoadingContext context, DbObject objectToLoad)
        {
            using (var connection = context.ConnectionData.GetConnection())
            {
                await connection.OpenAsync(context.Token);

                var command = _components.Creator.CreateCommand();
                command.Connection = connection;
                command.CommandText = _components.Loader.ScriptProvider.ProvideNameScript(objectToLoad, Type);

                using (var reader = await command.ExecuteReaderAsync(context.Token))
                {
                    while (await reader.ReadAsync())
                    {
                        objectToLoad.AddChild(CreateObject(reader));
                    }
                }
            }
        }

        protected virtual DbObject CreateObject(DbDataReader reader)
        {
            var name = reader.GetString(reader.GetOrdinal(Name));
            return MetadataTypeFactory.Instance.Create(Type, name);
        }

        public Task LoadDefinition(ILoadingContext context, DefinitionObject objectToLoad)
        {
            return Task.CompletedTask;
        }

        public async Task LoadProperties(ILoadingContext context, DbObject objectToLoad)
        {
            using (var connection = context.ConnectionData.GetConnection())
            {
                await connection.OpenAsync(context.Token);

                string query = _components.Loader.ScriptProvider.ProvidePropertiesScript(objectToLoad);

                var command = _components.Creator.CreateCommand();
                command.Connection = connection;
                command.CommandText = query;

                using (var reader = await command.ExecuteReaderAsync(context.Token))
                {
                    reader.Read();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.HasRows)
                            objectToLoad.Properties.Add(reader.GetName(i), reader.GetValue(i));
                    }
                }

            }
        }
    }
}
