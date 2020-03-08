using DBManager.Application.Providers;
using DBManager.Application.Providers.Abstract;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Default.Loaders;

namespace DBManager.Application
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        public App()
        {
            RegisterComponents();
        }


        private void RegisterComponents()
        {
            var kernel = ViewModelBase.Resolver;

            kernel.Bind<IConnectionProvider>().To<ConnectionProvider>().InSingletonScope();
            kernel.Bind<IComponentProvider>().To<ComponentProvider>().InSingletonScope();
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
        }
    }
}
