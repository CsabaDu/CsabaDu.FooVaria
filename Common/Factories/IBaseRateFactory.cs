namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IQuantifiableFactory/*<IBaseRate, IMeasurable>*/
{
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures);
}