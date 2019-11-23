using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        public static IKernel Kernel { get; } = new StandardKernel();
    }
}
