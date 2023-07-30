namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IExtent : IMeasure, IConvertMeasure<IExtent, IDistance>
{
    IExtent GetExtent(double quantity, ExtentUnit extentUnit);
    IExtent GetExtent(ValueType quantity, IMeasurement measurement);
    IExtent GetExtent(IBaseMeasure baseMeasure);
    IExtent GetExtent(IExtent? other = null);
}
