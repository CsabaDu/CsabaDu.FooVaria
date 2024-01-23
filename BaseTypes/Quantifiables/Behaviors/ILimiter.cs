namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface ILimiter
    {
        LimitMode LimitMode { get; init; }

        MeasureUnitCode GetLimiterMeasureUnitCode();
        decimal GetLimiterDefaultQuantity();
    }

    public interface ILimiter<in TSelf> : ILimiter, IEqualityComparer<TSelf>
        where TSelf : class, IQuantifiable, ILimiter
    {
        LimitMode GetLimitMode(TSelf limiter);
        bool? Includes(ILimitable limitable);

        void ValidateLimitMode(LimitMode limitMode);
    }
}
