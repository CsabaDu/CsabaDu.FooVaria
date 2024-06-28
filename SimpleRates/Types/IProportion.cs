using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.SimpleRates.Types;

public interface IProportion : ISimpleRate, IConcrete<IProportionFactory>
{
    //IProportionFactory Factory { get; init; }

    IProportion GetProportion(IQuantifiable numerator, IQuantifiable denominator);
    IProportion GetProportion(IQuantifiable numerator, IBaseMeasurement denominator);
    IProportion GetProportion(IQuantifiable numerator, Enum denominator);
    IProportion GetProportion(Enum numeratorContext, decimal quantity, Enum denominator);
    IProportion GetProportion(IBaseRate baseRate);
}
