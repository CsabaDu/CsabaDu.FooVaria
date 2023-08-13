namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ILimiter<in T, in U> : IEqualityComparer<T> where T : class, IQuantifiable where U : class, ILimitable
{
    LimitMode GetLimitMode(T? limiter = null);
    bool? Includes(U limitable);

    void ValidateLimitMode(LimitMode limitMode);
}
