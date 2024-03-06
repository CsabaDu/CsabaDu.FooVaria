
namespace CsabaDu.FooVaria.Rates.Types;

public interface IRate : IBaseRate, IExchange<IRate, IMeasurable>, IDenominate<IMeasure, IQuantifiable>, IEqualityComparer<IRate>
{
    IDenominator Denominator { get; init; }
    IMeasure Numerator { get; init; }
    IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

    IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode);
    ILimit? GetLimit();
    IRate GetRate(params IBaseMeasure[] baseMeasures);
    IRate GetRate(IRate rate);
}

    //IRate GetRate(IBaseRate rate);