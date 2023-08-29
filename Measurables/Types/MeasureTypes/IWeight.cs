namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IWeight : IMeasure, IVolumetricWeight<IWeight>
{
    IWeight GetWeight(double quantity, WeightUnit weightUnit);
    IWeight GetWeight(ValueType quantity, string name);
    IWeight GetWeight(ValueType quantity, IMeasurement measurement);
    IWeight GetWeight(IBaseMeasure baseMeasure);
    IWeight GetWeight(IWeight? other = null);
}
