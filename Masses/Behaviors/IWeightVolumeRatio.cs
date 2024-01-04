namespace Masses.Behaviors
{
    public interface IWeightVolumeRatio
    {
        IWeight GetVolumeWeight(decimal ratio);
        MeasureUnitTypeCode GetMeasureUnitTypeCode(decimal ratio);
        double GetQuantity(decimal ratio);
        decimal GetDefaultQuantity(decimal ratio);
    }
}
