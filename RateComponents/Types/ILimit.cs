using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.RateComponents.Types;

public interface ILimit : IRateComponent<ILimit, ulong>, ILimiter<ILimit, IBaseMeasure>, IGetFactory<ILimitFactory>
{
    //ILimitFactory Factory { get; init; }
    LimitMode LimitMode { get; init; }

    ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode);
    ILimit? GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode);
    ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode);
    ILimit? GetLimit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(IMeasurement measurement, ulong quantity, LimitMode limitMode);
    ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode);
    //ILimit GetLimit(ILimit other);
    ILimit GetLimit(ulong quantity);
}
