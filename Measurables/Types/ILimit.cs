namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimit : IBaseMeasure, ILimiter<ILimit, IMeasure>
{
    LimitMode LimitMode { get; init; }

    ILimit GetLimit(Enum measureUnit, decimal? exchangeRate = null, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(IMeasurement measurement, ValueType? quantity = null, LimitMode? limitMode = null);
    ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode? limitMode = null);
    ILimit GetLimit(ILimit? other = null);

    ulong GetValidLimitQuantity(ValueType? quantity);
}
