namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

public interface IConcreteFactory<TFactory>
    where TFactory : class, IFactory
{
    TFactory GetFactory();
}
