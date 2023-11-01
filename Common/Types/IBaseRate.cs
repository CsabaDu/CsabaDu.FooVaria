namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseRate : IBaseMeasurable, IMeasureUnitType/*, IExchange<IBaseRate, IBaseMeasurable>*/, IQuantifiable<decimal>, IProportional<IBaseRate>
{
    MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();

    IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator);
}
