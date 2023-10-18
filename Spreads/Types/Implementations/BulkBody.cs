namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class BulkBody : Spread<IVolume, VolumeUnit>, IBulkBody
{
    #region Constructors
    public BulkBody(IBulkBody other) : base(other)
    {
    }

    public BulkBody(IBulkBodyFactory factory, IVolume volume) : base(factory, volume)
    {
    }
    #endregion

    #region Public methods
    public IBulkBody GetBulkBody(IExtent radius, IExtent height)
    {
        return GetSpread(radius, height);
    }

    public IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height)
    {
        return GetSpread(length, width, height);
    }

    #region Override methods
    public override sealed IBulkBody? ExchangeTo(Enum measureUnit)
    {
        IVolume? exchanged = (IVolume?)SpreadMeasure.ExchangeTo(measureUnit);

        if (exchanged == null) return null;

        return GetFactory().Create(exchanged);
    }

    public override IBulkBodyFactory GetFactory()
    {
        return (IBulkBodyFactory)Factory;
    }

    public override IBulkBody GetSpread(IVolume volume)
    {
        return GetFactory().Create(volume);
    }

    public override IBulkBody GetSpread(VolumeUnit measureUnit)
    {
        return (IBulkBody?)ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override IBulkBody GetSpread(ISpreadMeasure spreadMeasure)
    {
        if (NullChecked(spreadMeasure, nameof(spreadMeasure)) is IVolume volume) return GetSpread(volume);

        throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure);
    }

    public override IBulkBody GetSpread(params IExtent[] shapeExtents)
    {
        return GetFactory().Create(shapeExtents);
    }

    public override IBulkBody GetSpread(IBaseSpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is IBody body) return GetSpread(body.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
