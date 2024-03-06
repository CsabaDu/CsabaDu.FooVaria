namespace CsabaDu.FooVaria.AbstractTypes.SimpleRates.Types;

public interface ISimpleRate : IBaseRate, IDenominate<IMeasure, Enum>
{
    Enum? this[RateComponentCode rateComponentCode] { get; }

    MeasureUnitCode NumeratorCode { get; init; }
    MeasureUnitCode DenominatorCode { get; init; }
    decimal DefaultQuantity { get; init; }

    IMeasureFactory GetMeasureFactory();
    decimal GetQuantity(Enum context, string paramName);
    decimal GetQuantity(Enum numerator, string numeratorName, Enum denominator, string denominatorName);
    ISimpleRate GetSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
}
