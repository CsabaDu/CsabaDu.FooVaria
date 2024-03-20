namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface ITryExchange<TSelf, in TContext> : IProportional<TSelf>, IExchangeable<TContext>
    where TSelf : class, IBaseQuantifiable
    where TContext : notnull
{
    bool TryExchangeTo(TContext context, [NotNullWhen(true)] out TSelf? exchanged);
}
