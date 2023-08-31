namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IVolume : IMeasure, IMeasure<IVolume, double, VolumeUnit>, ISpreadMeasure, IConvertMeasure<IVolume, IWeight>
{
}
