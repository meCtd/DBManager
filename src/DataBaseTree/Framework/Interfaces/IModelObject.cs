namespace DBManager.Application.Framework.Interfaces
{
    interface IModelObject<out T>
    {
        T Model { get; }
    }
}
