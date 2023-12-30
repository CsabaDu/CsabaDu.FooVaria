namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IBaseMeasureFactory/*<IBaseRate, IMeasurable>*/
{
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasure denominator);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
}