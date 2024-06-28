namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

public interface IConcrete<TFactory>
    where TFactory : class, IConcreteFactory
{
    TFactory GetFactory();
}
