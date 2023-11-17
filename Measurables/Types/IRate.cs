namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRate : IMeasurable, IBaseRate, IExchange<IBaseRate, IBaseMeasurable>
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IRateComponent? this[RateComponentCode rateComponentCode] { get; }

    ILimit? GetLimit();
    IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit);
    IRate GetRate(IRate other);
    IRateComponent? GetRateComponent(RateComponentCode rateComponentCode);
}
