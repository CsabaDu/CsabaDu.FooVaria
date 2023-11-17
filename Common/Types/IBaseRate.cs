namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseRate : IBaseMeasureTemp, IQuantifiable<decimal>, IProportional<IBaseRate>, IDenominate
{
    MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();

    IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IBaseMeasureTemp numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IBaseMeasureTemp numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IBaseMeasureTemp numerator, IBaseMeasurable denominator);
}
