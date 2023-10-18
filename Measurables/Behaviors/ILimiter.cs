namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ILimiter<in T, in U> : IEqualityComparer<T> where T : class, IQuantity where U : class, ILimitable
{
    LimitMode GetLimitMode(T limiter);

    bool? Includes(U limitable);

    void ValidateLimitMode(LimitMode limitMode);
}
