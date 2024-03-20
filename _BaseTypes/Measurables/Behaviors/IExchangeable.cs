namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IExchangeable<in TContext>
    where TContext : notnull
{
    bool IsExchangeableTo(TContext context);
}