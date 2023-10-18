namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseRate : IBaseMeasurable, IQuantifiable
    {
        MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
    }
}
