namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IExchangeable<in TContext>
    where TContext : notnull
{
    bool IsExchangeableTo(TContext? context);
}