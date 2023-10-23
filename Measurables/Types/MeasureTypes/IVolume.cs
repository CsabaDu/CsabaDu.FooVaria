namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IVolume : IMeasure<IVolume, double, VolumeUnit>, ISpreadMeasure, IConvertMeasure<IVolume, IWeight>
{
}
