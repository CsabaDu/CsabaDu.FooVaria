namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IDistance : IMeasure, IConvertMeasure<IDistance, IExtent, DistanceUnit>
{
    IDistance GetDistance(double quantity, DistanceUnit distanceUnit);
    IDistance GetDistance(ValueType quantity, string name);
    IDistance GetDistance(ValueType quantity, IMeasurement measurement);
    IDistance GetDistance(IBaseMeasure baseMeasure);
    IDistance GetDistance(IDistance? other = null);
}
