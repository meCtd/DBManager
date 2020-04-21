using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;
using Microsoft.Office.Interop.Access.Dao;

namespace DBManager.Access.Loader.AtomicLoaders
{
    class AccessTableLoader : IAtomicLoader
    {
        public MetadataType Type => MetadataType.Table;

        public async Task LoadChildren(ILoadingContext context, DbObject objectToLoad)
        {
            var connectionData = (AccessConnectionData)context.ConnectionData;

            await Task.Run(() =>
            {
                using (var connection = (AccessDbConnection)connectionData.GetConnection())
                {
                    connection.Open();

                    foreach (TableDef item in connection.DaoDatabase.TableDefs)
                    {
                        objectToLoad.AddChild(new Table(item.Name));
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
