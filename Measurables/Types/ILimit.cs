namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimit : IRateComponent, IMeasureUnit<Enum>, IDefaultBaseMeasure<ILimit, ulong>, ILimiter<ILimit, IMeasure>
{
    LimitMode LimitMode { get; init; }

    ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode);
    ILimit GetLimit(IRateComponent baseMeasure, LimitMode limitMode);
    ILimit GetLimit(ILimit other);
    ILimit GetLimit(ValueType quantity);
}
