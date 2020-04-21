using System;
using System.Threading.Tasks;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

using DaoIndex = Microsoft.Office.Interop.Access.Dao.Index;
using Index = DBManager.Default.Tree.DbEntities.Index;

namespace DBManager.Access.Loader.AtomicLoaders
{
    class AccessIndexLoader : IAtomicLoader
    {
        public MetadataType Type => MetadataType.Index;

        public async Task LoadChildren(ILoadingContext context, DbObject objectToLoad)
        {
            var connectionData = (AccessConnectionData)context.ConnectionData;

            await Task.Run(() =>
            {
                using (var connection = (AccessDbConnection)connectionData.GetConnection())
                {
                    connection.Open();

                    foreach (DaoIndex item in connection.DaoDatabase.TableDefs[objectToLoad.Name].Indexes)
                    {
                        var index = new Index(item.Name)
                        {
                            IsPrimaryKey = item.Primary,
                            IsUniqueConstraint = item.Unique,
                        };

                        objectToLoad.AddChild(index);
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
