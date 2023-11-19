using CsabaDu.FooVaria.Common.Behaviors;

namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IMeasurableFactory
{
    IBaseRate Create(IBaseMeasure numerator, IMeasurable denominator);
    IBaseRate Create(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate Create(IBaseMeasure numerator, Enum denominatorMeasureUnit);
}