using System.Globalization;

using DBManager.Application.Controls;
using DBManager.Application.Utils;

using Framework.Utils;

using AppContext = DBManager.Application.ViewModels.General.AppContext;


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
            AppContext.Current.Resolver.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            AppContext.Current.Resolver.Bind<IAsyncAwaiter>().To<AsyncAwaiter>().InSingletonScope();

            PluginManager.New().Load();

            SqlEditor.RegisterHighlights();
        }
    }
}
