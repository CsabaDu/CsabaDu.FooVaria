namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class BulkSurface : Spread<IBulkSurface, IArea, AreaUnit>, IBulkSurface
{
    #region Constructors
    internal BulkSurface(IBulkSurface other) : base(other)
    {
    }

    internal BulkSurface(IBulkSurfaceFactory factory, IArea area) : base(factory, area)
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

    public ISurface GetSurface()
    {
        return this;
    }

    #region Override methods
    public override IBulkSurfaceFactory GetFactory()
    {
        return (IBulkSurfaceFactory)Factory;
    }

    public override IBulkSurface GetSpread(IBaseSpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is ISurface surface) return GetSpread(surface.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
