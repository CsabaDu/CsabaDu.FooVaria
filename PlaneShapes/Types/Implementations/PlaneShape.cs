namespace CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

internal abstract class PlaneShape : SimpleShape, IPlaneShape
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaneShape"/> class by copying another plane shape.
    /// </summary>
    /// <param name="other">The other plane shape to copy.</param>
    private protected PlaneShape(IPlaneShape other) : base(other)
    {
        Area = other.Area;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaneShape"/> class using a factory and shape extents.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="shapeExtents">The shape extents.</param>
    private protected PlaneShape(IPlaneShapeFactory factory, params IExtent[] shapeExtents) : base(factory)
    {
        Area = (IArea)GetSpreadMeasure(shapeExtents);
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the area of the plane shape.
    /// </summary>
    public IArea Area { get; init; }
    #endregion

    #region Public methods
    /// <summary>
    /// Gets the surface of the plane shape.
    /// </summary>
    /// <returns>The surface of the plane shape.</returns>
    public ISurface GetSurface()
    {
        return this;
    }

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Gets the base measure unit of the plane shape.
    /// </summary>
    /// <returns>The base measure unit.</returns>
    public override sealed Enum GetBaseMeasureUnit()
    {
        return Area.GetMeasureUnit();
    }

    /// <summary>
    /// Gets a valid shape component from a quantifiable object.
    /// </summary>
    /// <param name="quantifiable">The quantifiable object.</param>
    /// <returns>The valid shape component, or null if not valid.</returns>
    public override sealed IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable)
    {
        if (quantifiable is not IExtent extent) return null;

        return extent;
    }

    /// <summary>
    /// Gets the bulk spread factory for the plane shape.
    /// </summary>
    /// <returns>The bulk spread factory.</returns>
    public override sealed IBulkSurfaceFactory GetBulkSpreadFactory()
    {
        IPlaneShapeFactory factory = (IPlaneShapeFactory)GetFactory();

        return factory.BulkSurfaceFactory;
    }

    /// <summary>
    /// Gets the spread measure of the plane shape.
    /// </summary>
    /// <returns>The spread measure.</returns>
    public override sealed IArea GetSpreadMeasure()
    {
        return Area;
    }

    /// <summary>
    /// Tries to exchange the plane shape to a specified context.
    /// </summary>
    /// <param name="context">The context to exchange to.</param>
    /// <param name="exchanged">The exchanged quantifiable object.</param>
    /// <returns>True if the exchange was successful, otherwise false.</returns>
    public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
        exchanged = measureUnitElements.MeasureUnit switch
        {
            AreaUnit areaUnit => GetSpread(Area.ExchangeTo(areaUnit)!),
            ExtentUnit extentUnit => ExchangeTo(extentUnit),

            _ => null,
        };

        return exchanged is not null;
    }
    #endregion
    #endregion
    #endregion
}
