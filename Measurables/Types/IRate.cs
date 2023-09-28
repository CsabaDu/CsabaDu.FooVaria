namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IQuantifiable, IProportional<IRate, IRate>, IExchange<IRate, IDenominator>, IMeasureUnitType
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    decimal GetDefaultQuantity();
    ILimit? GetLimit();

    IRate GetRate(IMeasure numerator, string customName, decimal quantity, ILimit limit);
    IRate GetRate(IMeasure numerator, string customName);
    IRate GetRate(IMeasure numerator, string customName, decimal quantity);
    IRate GetRate(IMeasure numerator, string customName, ILimit limit);

    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal quantity, ILimit limit);
    IRate GetRate(IMeasure numerator, Enum measureUnit);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal quantity);
    IRate GetRate(IMeasure numerator, Enum measureUnit, ILimit limit);

    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal quantity, ILimit limit);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal quantity);
    IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ILimit limit);

    IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal quantity, ILimit limit);
    IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal quantity);
    IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ILimit limit);

    IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal quantity, ILimit limit);
    IRate GetRate(IMeasure numerator, IMeasurement measurement);
    IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal quantity);
    IRate GetRate(IMeasure numerator, IMeasurement measurement, ILimit limit);

    IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit limit);
    IRate GetRate(IMeasure numerator);
    IRate GetRate(IMeasure numerator, IDenominator denominator);
    IRate GetRate(IMeasure numerator, ILimit limit);

    IRate GetRate(IRate other);
    IRate GetRate(IRateFactory rateFactory, IRate rate);

    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
    //IRateFactory GetFactory();
}
