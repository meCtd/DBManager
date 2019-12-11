using DBManager.Default;

namespace DBManager.Application.Providers.Abstract
{
    interface IComponentProvider
    {
        IDialectComponent ProvideComponent(DialectType dialect);
    }
}
