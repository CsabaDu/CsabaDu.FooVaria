namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class BulkBody : Spread<IBulkBody, IVolume, VolumeUnit>, IBulkBody
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
        return this;
    }

    public IBulkBody GetBulkBody(IExtent radius, IExtent height)
    {
        return GetSpread(radius, height);
    }

    public IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height)
    {
        return GetSpread(length, width, height);
    }

    #region Override methods
    public override IBulkBodyFactory GetFactory()
    {
        return (IBulkBodyFactory)Factory;
    }

    public override IBulkBody GetSpread(IBaseSpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is IBody body) return GetSpread(body.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
