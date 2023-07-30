namespace CsabaDu.FooVaria.Measurables.Types;

public interface IDenominator : IBaseMeasure
{
    IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = default, decimal? exchangeRate = null);
    IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = default);
    IDenominator GetDenominator(IBaseMeasure baseMeasure);
    IDenominator GetDenominator(IDenominator? other = null);

    decimal GetValidDenominatorQuantity(ValueType? quantity);
}
