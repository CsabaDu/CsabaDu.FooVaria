
namespace CsabaDu.FooVaria.Rates.Types;

public interface IRate : IBaseRate, ITryExchange<IRate, Enum>, ITryExchange<IRate, IBaseMeasure>, IDenominate<IMeasure, IMeasurable>, IEqualityComparer<IRate>
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode);
    ILimit? GetLimit();
    IRate GetRate(params IBaseMeasure[] baseMeasures);
    IRate GetRate(IRate rate);
}
