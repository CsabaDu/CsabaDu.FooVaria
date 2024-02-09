namespace CsabaDu.FooVaria.BulkSpreads.Types.Implementations;

internal sealed class BulkBody : BulkSpread<IBulkBody, IVolume, VolumeUnit>, IBulkBody
{
    #region Constructors
    internal BulkBody(IBulkBody other) : base(other)
    {
    }

    internal BulkBody(IBulkBodyFactory factory, IVolume volume) : base(factory, volume)
    {
    }
    #endregion

    #region Public methods
    public IBody GetBody()
    {
        return GetNew(this);
    }

    public IBulkBody GetBulkBody(IExtent radius, IExtent height)
    {
        return GetBulkSpread(radius, height);
    }

    public IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height)
    {
        return GetBulkSpread(length, width, height);
    }

    #region Override methods
    public override IBulkBodyFactory GetFactory()
    {
        return (IBulkBodyFactory)Factory;
    }

    public override IBulkBody GetBulkSpread(ISpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is IBody body) return GetBulkSpread(body.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
