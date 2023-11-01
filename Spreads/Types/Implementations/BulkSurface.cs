namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class BulkSurface : Spread<IBulkSurface, IArea, AreaUnit>, IBulkSurface
{
    #region Constructors
    public BulkSurface(IBulkSurface other) : base(other)
    {
    }

    public BulkSurface(IBulkSurfaceFactory factory, IArea area) : base(factory, area)
    {
    }
    #endregion

    #region Public methods
    public IBulkSurface GetBulkSurface(IExtent radius)
    {
        return GetSpread(radius);
    }

    public IBulkSurface GetBulkSurface(IExtent length, IExtent width)
    {
        return GetSpread(length, width);
    }

    #region Override methods
    public override IBulkSurfaceFactory GetFactory()
    {
        return (IBulkSurfaceFactory)Factory;
    }

    public override AreaUnit GetMeasureUnit()
    {
        return SpreadMeasure.GetMeasureUnit();
    }

    public override IBulkSurface GetSpread(IArea area)
    {
        return GetFactory().Create(area);
    }

    public override IBulkSurface GetSpread(AreaUnit measureUnit)
    {
        return (IBulkSurface?)ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override IBulkSurface GetSpread(IBaseSpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is ISurface surface) return GetSpread(surface.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
