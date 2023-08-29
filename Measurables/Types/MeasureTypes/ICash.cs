namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface ICash : IMeasure, ICustomMeasure<ICash, decimal, Currency>
{
    ICash GetCash(ValueType quantity, string name);
    ICash GetCash(decimal quantity, Currency currency);
    ICash GetCash(decimal quantity, Currency currency, decimal exchangeRate, string customName);
    ICash GetCash(ValueType quantity, string customName,MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    ICash GetCash(ValueType quantity, IMeasurement measurement);
    ICash GetCash(IBaseMeasure baseMeasure);
    ICash GetCash(ICash? other = null);
}
