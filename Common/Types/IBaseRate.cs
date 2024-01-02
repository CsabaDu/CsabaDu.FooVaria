namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseRate : IBaseMeasure/*<IBaseRate, IMeasurable>*/, IQuantity<decimal>, IProportional<IBaseRate>, IDenominate
{
    MeasureUnitTypeCode? this[RateComponentCode rateComponentCode] { get; }

    MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();

    IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures);
}
