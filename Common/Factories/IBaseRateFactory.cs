namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IBaseMeasureFactory<IBaseRate>
{
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IMeasurable denominator);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
}