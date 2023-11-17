using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchange<T, in U> : IProportional<T>, IExchangeable<U> where T : class, IBaseMeasurable, IQuantifiable where U : notnull
{
    T? ExchangeTo(U context);

    bool TryExchangeTo(U context, [NotNullWhen(true)] out T? exchanged);
}