using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchange<T, in U> : IProportional<T>, IExchangeable<U> where T : class, IMeasurable, IQuantifiable where U : notnull
{
    T? ExchangeTo(U context);

    public bool TryExchangeTo(U context, [NotNullWhen(true)] out T? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
    }
}