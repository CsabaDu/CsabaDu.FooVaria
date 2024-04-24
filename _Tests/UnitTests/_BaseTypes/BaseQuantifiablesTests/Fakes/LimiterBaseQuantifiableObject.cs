//namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests.Fakes;

//internal sealed class LimiterBaseQuantifiableObject(IRootObject rootObject, string paramName) : BaseQuantifiableChild(rootObject, paramName), ILimiter
//{
//    internal static LimiterBaseQuantifiableObject GetLimiterBaseQuantifiableObject(LimitMode? limitMode, Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
//    {
//        return new(Fields.RootObject, Fields.paramName)
//        {
//            LimitMode = limitMode,
//            Return = new()
//            {
//                GetBaseMeasureUnit = measureUnit,
//                GetDefaultQuantity = defaultQuantity,
//                GetFactory = factory,
//            }
//        };
//    }
//    public LimitMode? LimitMode { private get; set; }

//    public decimal GetLimiterDefaultQuantity() => GetDefaultQuantity();

//    public MeasureUnitCode GetLimiterMeasureUnitCode() => GetMeasureUnitCode();

//    public LimitMode? GetLimitMode() => LimitMode;
//}
