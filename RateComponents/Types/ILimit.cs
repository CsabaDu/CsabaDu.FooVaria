namespace CsabaDu.FooVaria.RateComponents.Types;

public interface ILimit : IRateComponent, IDefaultRateComponent<ILimit, ulong>, ILimiter<ILimit, IMeasure>
{
    LimitMode LimitMode { get; init; }

    ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode);
    ILimit? GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode);
    ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode);
    ILimit? GetLimit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(IRateComponent baseMeasure, LimitMode limitMode);
    ILimit GetLimit(ILimit other);
    ILimit GetLimit(ulong quantity);
}
