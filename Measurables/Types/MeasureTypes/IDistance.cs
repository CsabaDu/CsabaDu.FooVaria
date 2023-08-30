namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IDistance : IMeasure, IMeasure<IDistance, double, DistanceUnit>, IConvertMeasure<IDistance, IExtent>
{
    //IDistance GetDistance(double quantity, DistanceUnit distanceUnit);
    //IDistance GetDistance(ValueType quantity, string name);
    //IDistance GetDistance(ValueType quantity, IMeasurement? measurement = null);
    IDistance GetDistance(IBaseMeasure baseMeasure);
    //IDistance GetDistance(IDistance? other = null);
}
