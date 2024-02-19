//namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
//{
//    public interface ILimiter : ILimitMode
//    {
//        MeasureUnitCode GetLimiterMeasureUnitCode();
//        decimal GetLimiterDefaultQuantity();
//    }

//    public interface ILimiter<TSelf, in TLimitable> : ILimiter, IEqualityComparer<TSelf>
//        where TSelf : class, IBaseQuantifiable, ILimiter
//        where TLimitable : class, IBaseQuantifiable, ILimitable
//    {
//        bool? Includes(TLimitable? limitable);

//        //public sealed LimitMode GetLimitMode(ILimiter limiter)
//        //{
//        //    LimitMode? limitMode = NullChecked(limiter, nameof(limiter)).GetLimitMode();

//        //    return limitMode!.Value;
//        //}
//        //TSelf GetLimiter(TLimitable limitable, LimitMode limitMode);
//    }
//}
