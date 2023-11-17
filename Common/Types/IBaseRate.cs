namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseRate : IBaseMeasureTemp, IQuantifiable<decimal>, IProportional<IBaseRate>, IDenominate
{
    MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();

    IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator);
}
