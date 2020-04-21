using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

using DBManager.Access.ADO;
using DBManager.Access.Connection;
using DBManager.Default.Loader;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

using Microsoft.Office.Interop.Access.Dao;

using DaoIndex = Microsoft.Office.Interop.Access.Dao.Index;

namespace DBManager.Access.Loader.AtomicLoaders
{
    class AccessConstraintLoader : IAtomicLoader
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

                    LoadCheckConstraints(connection, (Table)objectToLoad);
                    LoadPrimaryKeys(connection, (Table)objectToLoad);
                    LoadForeignKeys(connection, (Table)objectToLoad);
                    LoadUniqueConstraints(connection, (Table)objectToLoad);
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

        #region Private members
        private void LoadCheckConstraints(AccessDbConnection connection, Table objectToLoad)
        {
            foreach (Field2 item in connection.DaoDatabase.TableDefs[objectToLoad.Name].Fields)
            {
                if (!string.IsNullOrWhiteSpace(item.ValidationRule))
                {
                    var constraint = new Constraint($"[{item.Name}].ValidationRule")
                    {
                        ConstraintType = ConstraintType.CheckConstraint,
                    };

                    objectToLoad.AddChild(constraint);
                }
            }

            LoadTableValidationRule(connection.DaoDatabase.TableDefs[objectToLoad.Name], objectToLoad);
        }

        private void LoadTableValidationRule(TableDef tableDef, Table objectToLoad)
        {
            if (!string.IsNullOrWhiteSpace(tableDef.ValidationRule))
            {
                var constraint = new Constraint($"[{tableDef.Name}].TableValidationRule")
                {
                    ConstraintType = ConstraintType.CheckConstraint,
                };

                objectToLoad.AddChild(constraint);
            }
        }

        private void LoadPrimaryKeys(AccessDbConnection connection, Table objectToLoad)
        {
            foreach (DaoIndex item in ((IEnumerable)connection.DaoDatabase.TableDefs[objectToLoad.Name].Indexes)
                                                        .Cast<DaoIndex>()
                                                        .Where(ind => ind.Primary))
            {
                var constraint = new Constraint($"PK_{objectToLoad.Name}")
                {
                    ConstraintType = ConstraintType.PrimaryKey
                };

                objectToLoad.AddChild(constraint);
            }
        }

        private void LoadForeignKeys(AccessDbConnection connection, Table objectToLoad)
        {
            foreach (DaoIndex item in ((IEnumerable)connection.DaoDatabase.TableDefs[objectToLoad.Name].Indexes)
                                                        .Cast<DaoIndex>()
                                                        .Where(ind => ind.Foreign))
            {
                var constraint = new Constraint($"FK_{objectToLoad.Name}_{item.Name}")
                {
                    ConstraintType = ConstraintType.ForeignKey
                };

                objectToLoad.AddChild(constraint);
            }
        }

        private void LoadUniqueConstraints(AccessDbConnection connection, Table objectToLoad)
        {
            foreach (DaoIndex item in ((IEnumerable)connection.DaoDatabase.TableDefs[objectToLoad.Name].Indexes)
                                                        .Cast<DaoIndex>()
                                                        .Where(ind => ind.Unique && !ind.Primary))
            {
                var constraint = new Constraint($"UK_{objectToLoad.Name}_{item.Name}")
                {
                    ConstraintType = ConstraintType.UniqueConstraint
                };

                objectToLoad.AddChild(constraint);
            }
        }
        #endregion
    }
}
