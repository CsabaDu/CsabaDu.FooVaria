namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IDenominatorFactory : IRateComponentFactory<IDenominator>, IBaseMeasureFactory<IDenominator>
{
    IDenominator Create(Enum context);
    IDenominator Create(string name);
    IDenominator Create(IMeasurement measurement);
    IDenominator Create(IBaseMeasure baseMeasure, ValueType quantity);
}
