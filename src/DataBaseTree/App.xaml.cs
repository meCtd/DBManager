using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Default;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loaders;
using DBManager.SqlServer;
using DBManager.SqlServer.Connection;
using DBManager.SqlServer.Provider;

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

            resolver.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            
            resolver.Bind<IConnectionData>().To<SqlServerConnectionData>()
                .InTransientScope()
                .Named(DialectType.SqlServer.ToString());

            resolver.Bind<IDialectComponent>().To<SqlServerComponent>()
                .InTransientScope()
                .Named(DialectType.SqlServer.ToString());


        }
    }
}
