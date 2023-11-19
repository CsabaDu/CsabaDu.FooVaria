namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IDenominatorFactory : IRateComponentFactory, IMeasurableFactory<IDenominator>
{
    IDenominator Create(string name);
    IDenominator Create(string name, ValueType quantity);
    IDenominator Create(Enum measureUnit);
    IDenominator Create(Enum measureUnit, ValueType quantity);
    IDenominator Create(IMeasurement measurement);
    IDenominator Create(IMeasurement measurement, ValueType quantity);
    IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
    IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    //IDenominator Create(IRateComponent baseMeasure);
    IDenominator Create(IDenominator denominator);
}

