namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IQuantifiable, IProportional<IRate, IRate>, IExchange<IRate, IDenominator>, IMeasureUnitType
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    decimal GetDefaultQuantity(IRate? rate = null);
    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, string customName, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, Enum measureUnit, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    IRate GetRate(IRate? other = null);
    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
}
