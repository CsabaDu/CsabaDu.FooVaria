namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface ICash : IMeasure, ICustomMeasure<ICash, decimal, Currency>
{
    ICash GetCash(decimal quantity, Currency currency, decimal? exchangeRate = null);
    ICash GetCash(ValueType quantity, IMeasurement measurement);
    ICash GetCash(IBaseMeasure baseMeasure);
    ICash GetCash(ICash? other = null);
    ICash GetCash(decimal quantity, decimal exchangeRate, string? customName = null);
}
