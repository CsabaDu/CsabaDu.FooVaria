namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchangeable<in TContext>
    where TContext : notnull
{
    bool IsExchangeableTo(TContext? context);
}