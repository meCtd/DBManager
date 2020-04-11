using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using System.Reflection;
using Application.Utils;
using DBManager.Application.Utils;

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

            PluginManager.New().Load();
        }


    }
}
