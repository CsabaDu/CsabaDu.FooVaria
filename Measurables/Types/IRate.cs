namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IProportional<IRate, IRate>, IExchange<IRate, IDenominator>, IMeasureUnitType
{
    IMeasure Numerator { get; init; }
    IDenominator Denominator { get; init; }

    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? exchangeRate = null, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    IRate GetRate(IRate? other = null);
    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
}
