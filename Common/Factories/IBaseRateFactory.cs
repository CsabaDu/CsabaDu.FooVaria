namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IBaseMeasureFactory<IBaseRate>
{
    IBaseRate Create(IBaseMeasure numerator, IMeasurable denominator);
    IBaseRate Create(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate Create(IBaseMeasure numerator, Enum denominatorMeasureUnit);
}