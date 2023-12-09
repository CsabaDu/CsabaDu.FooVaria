namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ILimiter<in TSelf, in TLimitable> : IEqualityComparer<TSelf> where TSelf : class, IBaseMeasure where TLimitable : class, IBaseMeasure
{
    LimitMode GetLimitMode(TSelf limiter);

    bool? Includes(TLimitable limitable);

    void ValidateLimitMode(LimitMode limitMode);
}
