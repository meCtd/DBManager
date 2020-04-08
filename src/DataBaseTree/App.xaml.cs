using System.Globalization;
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

            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            RegisterComponents();
        }


        private void RegisterComponents()
        {
            var resolver = ViewModelBase.Resolver;

            resolver.Bind<IConnectionProvider>().To<ConnectionProvider>().InSingletonScope();
            resolver.Bind<IComponentProvider>().To<ComponentProvider>().InSingletonScope();
            resolver.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
        }
    }
}
