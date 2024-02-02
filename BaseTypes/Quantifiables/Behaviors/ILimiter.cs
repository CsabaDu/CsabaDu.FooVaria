namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface ILimiter
    {
        LimitMode LimitMode { get; init; }

        MeasureUnitCode GetLimiterMeasureUnitCode();
        decimal GetLimiterDefaultQuantity();
    }

    public interface ILimiter<TSelf, in TLimitable> : ILimiter, IEqualityComparer<TSelf>
        where TSelf : class, IQuantifiable, ILimiter
        where TLimitable : class, IQuantifiable, ILimitable
    {
        bool? Includes(TLimitable? limitable);
        LimitMode GetLimitMode(ILimiter limiter);
        //TSelf GetLimiter(TLimitable limitable, LimitMode limitMode);
    }
}
