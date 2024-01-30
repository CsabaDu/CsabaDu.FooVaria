namespace CsabaDu.FooVaria.Rates.Types;

public interface IRate : IBaseRate, IExchange<IRate, IMeasurable>, IDenominate<IMeasure, IBaseMeasure>, IEqualityComparer<IRate>
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    new IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    ILimit? GetLimit();
    IRate GetRate(params IBaseMeasure[] baseMeasures);
    IRate GetRate(IBaseRate baseRate);
    IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode);
}
