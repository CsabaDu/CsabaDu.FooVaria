namespace CsabaDu.FooVaria.Quantifiables.Behaviors
{
    public interface ILimiter
    {
        LimitMode LimitMode { get; init; }

        MeasureUnitCode GetLimiterMeasureUnitCode();
        decimal GetLimiterDefaultQuantity();
    }

    public interface ILimiter<in TSelf, in TLimitable> : ILimiter, IEqualityComparer<TSelf>
        where TSelf : class, IQuantifiable, ILimiter
        where TLimitable : class, IBaseMeasure, ILimitable
    {
        LimitMode GetLimitMode(TSelf limiter);
        bool? Includes(TLimitable limitable);

        void ValidateLimitMode(LimitMode limitMode);
    }
}
