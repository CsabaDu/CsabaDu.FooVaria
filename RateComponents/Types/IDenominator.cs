namespace CsabaDu.FooVaria.RateComponents.Types;

public interface IDenominator : IRateComponent<IDenominator, decimal>, IMeasureUnit<Enum>/*, IDefaultRateComponent<IDenominator, decimal>*/
{
    IDenominator GetDenominator(Enum measureUnit);
    IDenominator GetDenominator(string name);
    IDenominator GetDenominator(IMeasurement measurement);
    IDenominator GetDenominator(IRateComponent rateComponent, ValueType quantity);
}

    //IDenominator GetDenominator(decimal quantity);
    //IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName);
    //IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    //IDenominator GetDenominator(Enum measureUnit, ValueType quantity); //
    //IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity); //
    //IDenominator GetDenominator(string name, ValueType quantity); //
    //IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    //IDenominator GetDenominator(IMeasurement measurement, ValueType quantity); //
    //IDenominator GetDenominator(IRateComponent rateComponent); //
    //IDenominator GetDenominator(IDenominator other); //
    //IDenominatorFactory GetFactory();