namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IDenominatorFactory : IBaseMeasureFactory
{
    IDenominator Create(Enum measureUnit, decimal exchangeRate, ValueType? quantity);
    IDenominator Create(Enum measureUnit, ValueType? quantity);
    IDenominator Create(IMeasurement measurement, ValueType? quantity);
    IDenominator Create(IDenominator other);
}
