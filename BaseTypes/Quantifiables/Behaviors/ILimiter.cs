using CsabaDu.FooVaria.Quantifiables.Enums;

namespace CsabaDu.FooVaria.Quantifiables.Behaviors
{
    public interface ILimiter
    {
    }

    public interface ILimiter<in TSelf, in TLimitable> : ILimiter, IEqualityComparer<TSelf>
        where TSelf : class, IBaseMeasure, ILimiter
        where TLimitable : class, IBaseMeasure
    {
        LimitMode GetLimitMode(TSelf limiter);

        bool? Includes(TLimitable limitable);

        void ValidateLimitMode(LimitMode limitMode);
    }
}
