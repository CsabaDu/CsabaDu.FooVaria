namespace CsabaDu.FooVaria.RateComponents.Types;

public interface IDenominator : IRateComponent<IDenominator, decimal>, IBaseMeasure<IDenominator>, IConcrete<IDenominatorFactory>
{
    //IDenominatorFactory Factory { get; init; }

    IDenominator GetDenominator(Enum measureUnit);
    IDenominator GetDenominator(string name);
    IDenominator GetDenominator(IMeasurement measurement);
    IDenominator GetDenominator(IBaseMeasure baseMeasure, ValueType quantity);
    IDenominator GetDenominator(IMeasurement measurement, decimal quantity);
}
