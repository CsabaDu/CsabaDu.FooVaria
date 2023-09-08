namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IQuantifiable, IProportional<IRate, IRate>, IExchange<IRate, IDenominator>, IMeasureUnitType
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    decimal GetDefaultQuantity();
    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, string customName, decimal? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null);
    IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    IRate GetRate(IRate? other = null);
    IRate GetRate(IRateFactory rateFactory, IRate rate);

    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
    IRateFactory GetRateFactory();
}
