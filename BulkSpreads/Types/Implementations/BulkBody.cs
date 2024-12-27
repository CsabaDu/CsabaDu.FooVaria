namespace CsabaDu.FooVaria.BulkSpreads.Types.Implementations;

internal sealed class BulkBody : BulkSpread<IBulkBody, IVolume, VolumeUnit>, IBulkBody
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BulkBody"/> class by copying another <see cref="IBulkBody"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="IBulkBody"/> instance to copy.</param>
    internal BulkBody(IBulkBody other) : base(other)
    {
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BulkBody"/> class with the specified factory and volume.
    /// </summary>
    /// <param name="factory">The factory used to create the <see cref="BulkBody"/>.</param>
    /// <param name="volume">The volume to use.</param>
    internal BulkBody(IBulkBodyFactory factory, IVolume volume) : base(factory, volume)
    {
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the factory associated with the <see cref="BulkBody"/>.
    /// </summary>
    public IBulkBodyFactory Factory { get; init; }
    #endregion

    #region Public methods
    /// <summary>
    /// Gets a new <see cref="IBody"/> instance.
    /// </summary>
    /// <returns>A new <see cref="IBody"/> instance.</returns>
    public IBody GetBody()
    {
        return GetNew(this);
    }

    /// <summary>
    /// Gets a new <see cref="IBulkBody"/> instance based on the specified radius and height.
    /// </summary>
    /// <param name="radius">The radius to use.</param>
    /// <param name="height">The height to use.</param>
    /// <returns>A new <see cref="IBulkBody"/> instance.</returns>
    public IBulkBody GetBulkBody(IExtent radius, IExtent height)
    {
        return GetBulkSpread(radius, height);
    }

    /// <summary>
    /// Gets a new <see cref="IBulkBody"/> instance based on the specified length, width, and height.
    /// </summary>
    /// <param name="length">The length to use.</param>
    /// <param name="width">The width to use.</param>
    /// <param name="height">The height to use.</param>
    /// <returns>A new <see cref="IBulkBody"/> instance.</returns>
    public IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height)
    {
        return GetBulkSpread(length, width, height);
    }

    #region Override methods
    /// <summary>
    /// Gets the factory associated with the <see cref="BulkBody"/>.
    /// </summary>
    /// <returns>The factory associated with the <see cref="BulkBody"/>.</returns>
    public override IBulkBodyFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets a new <see cref="IBulkBody"/> instance based on the specified spread.
    /// </summary>
    /// <param name="baseSppread">The spread to use.</param>
    /// <returns>A new <see cref="IBulkBody"/> instance.</returns>
    /// <exception cref="ArgumentTypeOutOfRangeException">Thrown if the spread is not valid.</exception>
    public override IBulkBody GetBulkSpread(ISpread baseSppread)
    {
        if (NullChecked(baseSppread, nameof(baseSppread)) is IBody body) return GetBulkSpread(body.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(baseSppread), baseSppread);
    }
    #endregion
    #endregion
}
