using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseTree.Model.Printers;
using DataBaseTree.Model.Tree;
using DBManager.Default.Loaders;

namespace DBManager.Default
{
    public interface IDialect
    {
        IPrinterFactory Printer { get; }

        IObjectLoader Loader { get; }

        Hierarchy GetHierarchy { get; }
    }
}
