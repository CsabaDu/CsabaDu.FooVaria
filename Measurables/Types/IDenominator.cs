namespace CsabaDu.FooVaria.Measurables.Types;

public interface IDenominator : IRateComponent, IMeasureUnit<Enum>, IDefaultBaseMeasure<IDenominator, decimal>
{
    IDenominator GetDenominator(Enum measureUnit);
    IDenominator GetDenominator(Enum measureUnit, ValueType quantity); //

    IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName);
    IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity); //

    IDenominator GetDenominator(string name);
    IDenominator GetDenominator(string name, ValueType quantity); //

    IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);

    IDenominator GetDenominator(IMeasurement measurement);
    IDenominator GetDenominator(IMeasurement measurement, ValueType quantity); //

    IDenominator GetDenominator(IRateComponent baseMeasure); //
    IDenominator GetDenominator(IRateComponent baseMeasure, ValueType quantity);

    IDenominator GetDenominator(IDenominator other); //

    //IDenominatorFactory GetFactory();
}
