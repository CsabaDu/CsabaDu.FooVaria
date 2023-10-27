namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IBaseRate
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit);
    IRate GetRate(IRate other);
    IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
}
