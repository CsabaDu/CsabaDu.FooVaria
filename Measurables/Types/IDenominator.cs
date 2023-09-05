namespace CsabaDu.FooVaria.Measurables.Types;

public interface IDenominator : IBaseMeasure
{
    IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = default);
    IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity = default);
    IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity = default);
    IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = default);
    IDenominator GetDenominator(IBaseMeasure baseMeasure);
    IDenominator GetDenominator(IDenominator? other = null);
    IDenominator GetDenominator(string name, ValueType? quantity = default);

    IDenominatorFactory GetDenominatorFactory();
}
