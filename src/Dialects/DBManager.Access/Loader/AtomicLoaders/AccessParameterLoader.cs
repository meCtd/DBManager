using System;
using System.Data;
using System.Threading.Tasks;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

using DaoParameter = Microsoft.Office.Interop.Access.Dao.Parameter;

namespace DBManager.Access.Loader.AtomicLoaders
{
    class AccessParameterLoader : IAtomicLoader
    {
        public MetadataType Type => MetadataType.Column;

        public async Task LoadChildren(ILoadingContext context, DbObject objectToLoad)
        {
            var connectionData = (AccessConnectionData)context.ConnectionData;

            await Task.Run(() =>
            {
                using (var connection = (AccessDbConnection)connectionData.GetConnection())
                {
                    connection.Open();

                    foreach (DaoParameter daoParameter in connection.DaoDatabase.QueryDefs[objectToLoad.Name].Parameters)
                    {
                        var param = new Parameter(daoParameter.Name)
                        {
                            Directon = (ParameterDirection)daoParameter.Direction,
                        };

                        objectToLoad.AddChild(param);
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
