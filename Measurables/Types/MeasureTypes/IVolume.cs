namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IVolume : IMeasure, ISpreadMeasure
{
    IVolume GetVolume(double quantity, VolumeUnit volumeUnit);
    IVolume GetVolume(ValueType quantity, string name);
    IVolume GetVolume(ValueType quantity, IMeasurement? measurement = null);
    IVolume GetVolume(IBaseMeasure baseMeasure);
    IVolume GetVolume(IVolume? other = null);

    IWeight ToVolumetricWeight(decimal ratio, WeightUnit weightUnit = default);
}
