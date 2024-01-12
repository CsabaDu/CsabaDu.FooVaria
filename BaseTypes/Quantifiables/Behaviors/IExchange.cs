namespace CsabaDu.FooVaria.Quantifiables.Behaviors;

public interface IExchange<TSelf, in TContext> : IProportional<TSelf>, IExchangeable<TContext>
    where TSelf : class, IQuantifiable
    where TContext : notnull
{
    TSelf? ExchangeTo(TContext context);

    #region Default implementations
    public bool TryExchangeTo(TContext context, [NotNullWhen(true)] out TSelf? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
    }
    #endregion
}