namespace CsabaDu.FooVaria.Masses.Behaviors;

public interface IVolumeWeightRatio : IVolumeWeight
{
    IWeight GetVolumeWeight(decimal ratio);
    MeasureUnitCode GetMeasureUnitCode(decimal ratio);
    double GetQuantity(decimal ratio);
    decimal GetDefaultQuantity(decimal ratio);
    IWeight GetChargeableWeight(decimal ratio, WeightUnit weightUnit, RoundingMode roundingMode);
}
