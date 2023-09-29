namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IQuantifiable, IProportional<IRate, IRate>, IExchange<IRate, IDenominator>, IMeasureUnitType
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    decimal GetRateDefaultQuantity();
    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit);
    IRate GetRate(IRate other);
    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
}
