namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimit : IBaseMeasure, ILimiter<ILimit, IMeasure>
{
    LimitMode LimitMode { get; init; }

    ILimit GetLimit(string name, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(IMeasurement measurement, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode? limitMode = null);
    ILimit GetLimit(ILimit? other = null, LimitMode? limitMode = null);

    ILimitFactory GetLimitFactory();
}
