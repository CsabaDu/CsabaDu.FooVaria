namespace CsabaDu.FooVaria.BulkSpreads.Factories.Implementations;

public sealed class BulkBodyFactory(IMeasureFactory measureFactory) : BulkSpreadFactory<IBulkBody, IVolume, VolumeUnit>(measureFactory), IBulkBodyFactory
{
    #region Public methods
    #region Override methods
    public override IBulkBody Create(IVolume volume)
    {
        return new BulkBody(this, volume);
    }

    public override IBulkBody CreateNew(IBulkBody other)
    {
        return new BulkBody(other);
    }

    public override IBulkBody Create(params IExtent[] shapeExtents)
    {
        IVolume volume = GetVolume(MeasureFactory, shapeExtents);

        return Create(volume);
    }

    public override IBulkBody Create(VolumeUnit volumeUnit, double quantity)
    {
        IVolume volume = CreateSpreadMeasure(volumeUnit, quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);

        return Create(volume);
    }

    public override IBulkBody? Create(ISpread spread)
    {
        if (spread?.GetSpreadMeasure() is not IVolume volume) return null;

        return Create(volume);
    }
    #endregion
    #endregion
}
