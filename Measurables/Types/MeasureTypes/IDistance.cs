namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IDistance : IMeasure, IMeasure<IDistance, double, DistanceUnit>, IConvertMeasure<IDistance, IExtent>
{
}
