using System;
using System.Collections.Generic;
using DBManager.Application.Providers.Abstract;
using DBManager.Default;
using DBManager.SqlServer;

namespace DBManager.Application.Providers
{
    public class ComponentProvider : IComponentProvider
    {
        private static readonly Dictionary<DialectType, IDialectComponent> _connectionCreator = new Dictionary<DialectType, IDialectComponent>
        {
            [DialectType.MsSql] = new SqlServerComponent()
        };
        
        public IDialectComponent ProvideComponent(DialectType dialect)
        {
            if (_connectionCreator.TryGetValue(dialect, out var result))
            {
                return result;
            }

            throw new NotSupportedException("Dialect is not supported yet!");
        }
    }
}
