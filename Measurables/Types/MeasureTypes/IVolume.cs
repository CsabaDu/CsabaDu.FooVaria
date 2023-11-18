namespace CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;

public interface IVolume : IMeasure<IVolume, double, VolumeUnit>, ISpreadMeasure, IConvertMeasure<IVolume, IWeight>
{
}
