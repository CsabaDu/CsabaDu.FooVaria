namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public sealed class BulkBodyFactory : SpreadFactory<IBulkBody, IVolume, VolumeUnit>, IBulkBodyFactory
{
    #region Constructors
    public BulkBodyFactory(IMeasureFactory measureFactory) : base(measureFactory)
    {
    }
    #endregion

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
        IVolume volume = SpreadMeasures.GetVolume(MeasureFactory, shapeExtents);

        return Create(volume);
    }

    public override IBulkBody Create(VolumeUnit volumeUnit, double quantity)
    {
        IVolume volume = (IVolume)MeasureFactory.Create(volumeUnit, quantity);

        return Create(volume);
    }

    public override IBulkBody? Create(IBaseSpread baseSpread)
    {
        if (baseSpread?.GetSpreadMeasure() is not IVolume volume) return null;

        return Create(volume);
    }
    #endregion
    #endregion
}

