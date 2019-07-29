using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Default.Loaders;
using DBManager.Default.Printers;
using DBManager.Default.Tree;

namespace DBManager.Default
{
    //TODO: Base dialect components that need to work with
    public interface IDialect
    {
        IPrinterFactory Printer { get; }

        IObjectLoader Loader { get; }

        Hierarchy GetHierarchy();
    }
}
