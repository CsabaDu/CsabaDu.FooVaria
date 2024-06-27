namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors
{
    public interface IConcreteFactory : IFactory;

    public interface IConcreteFactory<TFactory>
        where TFactory : class, IConcreteFactory
    {
        TFactory GetFactory();
    }
}
