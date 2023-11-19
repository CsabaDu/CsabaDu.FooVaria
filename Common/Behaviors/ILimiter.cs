namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ILimiter<in T, in U> : IEqualityComparer<T> where T : class, IBaseMeasure where U : class, IBaseMeasure
{
    LimitMode GetLimitMode(T limiter);

    bool? Includes(U limitable);

    void ValidateLimitMode(LimitMode limitMode);
}
