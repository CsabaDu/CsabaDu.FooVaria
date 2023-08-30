namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IExtent : IMeasure, IMeasure<IExtent, double, ExtentUnit>, IConvertMeasure<IExtent, IDistance>
{
    //IExtent GetExtent(double quantity, ExtentUnit extentUnit);
    //IExtent GetExtent(ValueType quantity, string name);
    //IExtent GetExtent(ValueType quantity, IMeasurement? measurement = null);
    IExtent GetExtent(IBaseMeasure baseMeasure);
    //IExtent GetExtent(IExtent? other = null);
}
