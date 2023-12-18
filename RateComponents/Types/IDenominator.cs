namespace CsabaDu.FooVaria.RateComponents.Types;

public interface IDenominator : IRateComponent<IDenominator, decimal>, IMeasureUnit<Enum>
{
    IDenominator GetDenominator(Enum measureUnit);
    IDenominator GetDenominator(string name);
    IDenominator GetDenominator(IMeasurement measurement);
    IDenominator GetDenominator(IRateComponent rateComponent, ValueType quantity);
}
