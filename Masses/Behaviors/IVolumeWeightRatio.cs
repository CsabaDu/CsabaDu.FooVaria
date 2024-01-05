namespace CsabaDu.FooVaria.Masses.Behaviors
{
    public interface IVolumeWeightRatio : IVolumeWeight
    {
        IWeight GetVolumeWeight(decimal ratio);
        MeasureUnitTypeCode GetMeasureUnitTypeCode(decimal ratio);
        double GetQuantity(decimal ratio);
        decimal GetDefaultQuantity(decimal ratio);
    }
}
