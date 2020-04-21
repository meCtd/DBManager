using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Access.Loader.AtomicLoaders.Wrapper;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

using Microsoft.Office.Interop.Access.Dao;

namespace DBManager.Access.Loader.AtomicLoaders
{
    class AccessViewLoader : IAtomicLoader
    {
        public MetadataType Type => MetadataType.View;

        public async Task LoadChildren(ILoadingContext context, DbObject objectToLoad)
        {
            var connectionData = (AccessConnectionData)context.ConnectionData;

            await Task.Run(() =>
            {
                using (var connection = (AccessDbConnection)connectionData.GetConnection())
                {
                    connection.Open();

                    foreach (QueryDefWrapper query in ((IEnumerable)connection.DaoDatabase.QueryDefs)
                                                        .Cast<QueryDef>()
                                                        .Select(queryDef => new QueryDefWrapper(queryDef))
                                                        .Where(query => query.IsView))
                    {
                        objectToLoad.AddChild(new View(query.Name));
                    }
                }
            });
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
