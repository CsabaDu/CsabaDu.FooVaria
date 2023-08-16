namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IQuantifiable, IProportional<IRate, IRate>, IExchange<IRate, IDenominator>, IMeasureUnitType
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }

    decimal GetDefaultQuantity();
    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? exchangeRate = null, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    IRate GetRate(IRate? other = null);
    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
}
