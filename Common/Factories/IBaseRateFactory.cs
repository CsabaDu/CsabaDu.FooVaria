using CsabaDu.FooVaria.Common.Behaviors;

namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IBaseMeasurableFactory
{
    IBaseRate Create(IBaseMeasureTemp numerator, IBaseMeasurable denominator);
    IBaseRate Create(IBaseMeasureTemp numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate Create(IBaseMeasureTemp numerator, Enum denominatorMeasureUnit);
}