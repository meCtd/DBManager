using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Access.Connection;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Access.Loader.AtomicLoaders
{
    class AccessDatabaseLoader : IAtomicLoader
    {
        public MetadataType Type => MetadataType.Database;

        public Task LoadChildren(ILoadingContext context, DbObject objectToLoad)
        {
            var connection = (AccessConnectionData)context.ConnectionData;
            var name = Path.GetFileNameWithoutExtension(connection.DataSource);

            objectToLoad.AddChild(new Database(name));

            return Task.CompletedTask;
        }

        public Task LoadDefinition(ILoadingContext context, DefinitionObject objectToLoad)
        {
            return Task.CompletedTask;
        }

        public Task LoadProperties(ILoadingContext context, DbObject objectToLoad)
        {
            throw new NotImplementedException();
        }
    }
}
