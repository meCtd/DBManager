using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using DBManager.Default;
using AppContext = DBManager.Application.ViewModels.General.AppContext;

namespace DBManager.Application.Utils
{
    internal class PluginManager
    {
        private const string DialectPath = @"Dialects";

        [ImportMany(typeof(IDialectComponent))]
        private IEnumerable<Lazy<IDialectComponent>> _components;

        public static PluginManager New() => new PluginManager();

        public void Load()
        {
            var catalog = new AggregateCatalog();

            var dialectsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DialectPath);

            catalog.Catalogs.Add(new DirectoryCatalog(dialectsPath));

            CompositionContainer container = new CompositionContainer(catalog);

            //Fill the imports of this object
            container.ComposeParts(this);

            foreach (var item in _components)
            {
                AppContext.Current.Initialize(item.Value);
            }
        }
    }
}
