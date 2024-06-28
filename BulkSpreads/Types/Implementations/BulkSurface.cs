namespace CsabaDu.FooVaria.BulkSpreads.Types.Implementations;

internal sealed class BulkSurface : BulkSpread<IBulkSurface, IArea, AreaUnit>, IBulkSurface
{
    #region Constructors
    internal BulkSurface(IBulkSurface other) : base(other)
    {
        //Factory = other.Factory;
    }

    internal BulkSurface(IBulkSurfaceFactory factory, IArea area) : base(factory, area)
    {
        //Factory = factory;
    }
    #endregion

    #region Properties
    //public IBulkSurfaceFactory Factory { get; init; }
    #endregion

    #region Public methods
    public IBulkSurface GetBulkSurface(IExtent radius)
    {
        return GetBulkSpread(radius);
    }

    public IBulkSurface GetBulkSurface(IExtent length, IExtent width)
    {
        return GetBulkSpread(length, width);
    }

    public ISurface GetSurface()
    {
        return GetNew();
    }

    #region Override methods
    public IBulkSurfaceFactory GetFactory()
    {
        return (IBulkSurfaceFactory)Factory;
    }

    public override IBulkSurface GetBulkSpread(ISpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is ISurface surface) return GetBulkSpread(surface.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
