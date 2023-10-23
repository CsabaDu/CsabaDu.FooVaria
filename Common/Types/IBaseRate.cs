namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseRate : IBaseMeasurable, IMeasureUnitType/*, IExchange<IBaseRate, IBaseMeasurable>*/, IQuantifiable, IProportional<IBaseRate>
    {
        MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();

        IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        IBaseRate GetBaseRate(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    }
}
