namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IVolume : IMeasure, IMeasure<IVolume, double, VolumeUnit>, ISpreadMeasure, IConvertMeasure<IVolume, IWeight>
{
}


    //IVolume GetVolume(IBaseMeasure baseMeasure);
    //IVolume GetVolume(double quantity, VolumeUnit volumeUnit);
    //IVolume GetVolume(ValueType quantity, string name);
    //IVolume GetVolume(ValueType quantity, IMeasurement? measurement = null);
    //IVolume GetVolume(IVolume? other = null);