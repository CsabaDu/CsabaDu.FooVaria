namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IWeight : IMeasure, IMeasure<IWeight, double, WeightUnit>, IConvertMeasure<IWeight, IVolume>
{
}
