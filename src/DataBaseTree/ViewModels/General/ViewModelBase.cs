using Ninject;
using Prism.Mvvm;

namespace DBManager.Application.ViewModels.General
{
    public class ViewModelBase : BindableBase
    {
        public static IKernel Kernel { get; } = new StandardKernel();
    }
}
