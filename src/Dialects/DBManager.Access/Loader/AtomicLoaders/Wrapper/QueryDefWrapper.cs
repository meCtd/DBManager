using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Access.Dao;

namespace DBManager.Access.Loader.AtomicLoaders.Wrapper
{
    class QueryDefWrapper
    {
        #region Properties

        public string Name { get; }
        public QueryDefTypeEnum Type { get; }
        public DateTime DateCreated { get; }
        public DateTime LastUpdated { get; }
        public bool IsUpdatable { get; }
        public bool IsView { get; }
        public int ParametersCount { get; }
        public string Sql { get; }

        #endregion

        #region C-tor

        public QueryDefWrapper(QueryDef daoQuery)
        {
            Name = daoQuery.Name;
            Type = (QueryDefTypeEnum)daoQuery.Type;
            DateCreated = (DateTime)daoQuery.DateCreated;
            LastUpdated = (DateTime)daoQuery.LastUpdated;
            IsUpdatable = daoQuery.Updatable;

            try
            {
                ParametersCount = daoQuery.Parameters.Count;
            }
            catch (COMException)
            {
                // Some queryDefs throws COMExceptions when refers to Parameters.Count property.
                // In this case ParametersCount property has default value (0).
            }

            IsView = (Type == QueryDefTypeEnum.dbQSelect || Type == QueryDefTypeEnum.dbQSetOperation) && ParametersCount == 0;
            Sql = daoQuery.SQL;
        }

        #endregion

    }
}
