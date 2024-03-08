namespace CsabaDu.FooVaria.Measures.Types;

public interface IVolume : IMeasure<IVolume, double, VolumeUnit>, ISpreadMeasure, IConvertMeasure<IVolume, IWeight>;
