namespace CsabaDu.FooVaria.Proportions.Types;

public interface IProportion : ISimpleRate
{
    IProportion GetProportion(IQuantifiable numerator, IQuantifiable denominator);
    IProportion GetProportion(IQuantifiable numerator, IBaseMeasurement denominator);
    IProportion GetProportion(IQuantifiable numerator, Enum denominator);
    IProportion GetProportion(Enum numeratorContext, decimal quantity, Enum denominator);
    IProportion GetProportion(IBaseRate baseRate);
}
