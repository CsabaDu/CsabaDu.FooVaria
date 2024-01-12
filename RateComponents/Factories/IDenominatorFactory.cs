namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IDenominatorFactory : IRateComponentFactory<IDenominator>, IDefaultBaseMeasureFactory<IDenominator>
{
    IDenominator Create(Enum measureUnit);
    IDenominator Create(string name);
    IDenominator Create(IMeasurement measurement);
    IDenominator Create(IRateComponent rateComponent, ValueType quantity);
}



    //IDenominator CreateNew(string name);
    //IDenominator CreateNew(string name, ValueType quantity);
    //IDenominator CreateNew(Enum measureUnit);
    //IDenominator CreateNew(Enum measureUnit, ValueType quantity);
    //IDenominator CreateNew(IMeasurement measurement);
    //IDenominator CreateNew(IMeasurement measurement, ValueType quantity);
    //IDenominator CreateNew(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
    //IDenominator CreateNew(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
    ////IDenominator CreateNew(IRateComponent rateComponent);
    //IDenominator CreateNew(IDenominator denominator);