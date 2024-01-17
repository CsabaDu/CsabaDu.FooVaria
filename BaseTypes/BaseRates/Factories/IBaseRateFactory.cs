namespace CsabaDu.FooVaria.BaseRates.Factories;

public interface IBaseRateFactory : IQuantifiableFactory/*<IBaseRate, IMeasurable>*/
{
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures);
}