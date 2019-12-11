using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;

namespace DBManager.Application.Providers.Abstract
{
    interface IConnectionProvider
    {
        ConnectionData ProvideConnection(DialectType dialect);
    }
}
