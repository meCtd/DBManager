using System.Collections.Generic;
using DBManager.Default;
using Ninject;

namespace DBManager.Application.ViewModels.General
{
    public class AppContext
    {
        private readonly HashSet<DialectType> _availableDialects = new HashSet<DialectType>();

        public static AppContext Current { get; } = new AppContext();

        public IKernel Resolver { get; } = new StandardKernel();

        public IEnumerable<DialectType> AvailableDialects => _availableDialects;

        public void Initialize(IDialectComponent component)
        {
            _availableDialects.Add(component.Type);

            Resolver.Bind<IDialectComponent>()
                .ToConstant(component)
                .Named(component.Type.ToString());
        }
    }
}
