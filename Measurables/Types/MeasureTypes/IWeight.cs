namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IWeight : IMeasure
{
    IWeight GetWeight(double quantity, WeightUnit weightUnit);
    IWeight GetWeight(ValueType quantity, string name);
    IWeight GetWeight(ValueType quantity, IMeasurement measurement);
    IWeight GetWeight(IBaseMeasure baseMeasure);
    IWeight GetWeight(IWeight? other = null);

    IWeight GetVolumetricWeight(IVolume volume, decimal ratio, WeightUnit weightUnit = default, bool isGreater = true);
}
