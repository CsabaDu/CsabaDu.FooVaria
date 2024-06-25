namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

public interface IGetFactory<TFactory>
    where TFactory : class, IFactory
{
    TFactory GetFactory();
}
