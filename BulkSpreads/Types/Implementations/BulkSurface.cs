namespace CsabaDu.FooVaria.BulkSpreads.Types.Implementations;

/// <summary>
/// Represents a two dimensions surface with undefined boundaries.
/// </summary>
internal sealed class BulkSurface : BulkSpread<IBulkSurface, IArea, AreaUnit>, IBulkSurface
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BulkSurface"/> class with the specified other bulk surface.
    /// </summary>
    /// <param name="other">The other bulk surface to copy.</param>
    internal BulkSurface(IBulkSurface other) : base(other)
    {
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BulkSurface"/> class with the specified factory and area.
    /// </summary>
    /// <param name="factory">The factory used to create the bulk surface.</param>
    /// <param name="area">The area to use.</param>
    internal BulkSurface(IBulkSurfaceFactory factory, IArea area) : base(factory, area)
    {
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the factory used to create the bulk surface.
    /// </summary>
    public IBulkSurfaceFactory Factory { get; init; }
    #endregion

    #region Public methods
    /// <summary>
    /// Gets a new bulk surface based on the specified radius.
    /// </summary>
    /// <param name="radius">The radius to use.</param>
    /// <returns>The new bulk surface.</returns>
    public IBulkSurface GetBulkSurface(IExtent radius)
    {
        return GetBulkSpread(radius);
    }

    /// <summary>
    /// Gets a new bulk surface based on the specified length and width.
    /// </summary>
    /// <param name="length">The length to use.</param>
    /// <param name="width">The width to use.</param>
    /// <returns>The new bulk surface.</returns>
    public IBulkSurface GetBulkSurface(IExtent length, IExtent width)
    {
        return GetBulkSpread(length, width);
    }

    /// <summary>
    /// Gets a new surface based on the current bulk surface.
    /// </summary>
    /// <returns>The new surface.</returns>
    public ISurface GetSurface()
    {
        return GetNew(this);
    }

    #region Override methods
    /// <summary>
    /// Gets the factory used to create the bulk surface.
    /// </summary>
    /// <returns>The factory.</returns>
    public override IBulkSurfaceFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets a new bulk surface based on the specified base spread.
    /// </summary>
    /// <param name="baseSppread">The base spread to use.</param>
    /// <returns>The new bulk surface.</returns>
    /// <exception cref="ArgumentTypeOutOfRangeException">Thrown if the base spread is not valid.</exception>
    public override IBulkSurface GetBulkSpread(ISpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is ISurface surface) return GetBulkSpread(surface.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
