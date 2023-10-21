namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseRate : IBaseMeasurable, IMeasureUnitType, IExchange<IBaseRate, IBaseMeasurable>
    {
        MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; }

        IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode);
    }
}
