namespace CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

internal abstract class PlaneShape : SimpleShape, IPlaneShape
{
    #region Constructor
    private protected PlaneShape(IPlaneShape other) : base(other)
    {
        Area = other.Area;
    }

    private protected PlaneShape(IPlaneShapeFactory factory, params IExtent[] shapeExtents) : base(factory)
    {
        Area = (IArea)GetSpreadMeasure(shapeExtents);
    }
    #endregion

    #region Properties
    public IArea Area { get; init; }
    #endregion

    #region Public methods
    public ISurface GetSurface()
    {
        return this;
    }

    #region Override methods
    #region Sealed methods
    public override sealed Enum GetBaseMeasureUnit()
    {
        return Area.GetMeasureUnit();
    }

    public override sealed IShapeComponent? GetValidShapeComponent(IBaseQuantifiable? shapeComponent)
    {
        if (shapeComponent is not IExtent extent) return null;

        return extent;
    }

    public override sealed IBulkSurfaceFactory GetBulkSpreadFactory()
    {
        IPlaneShapeFactory factory = (IPlaneShapeFactory)GetFactory();

        return factory.BulkSurfaceFactory;
    }

    public override sealed IPlaneShape GetShape()
    {
        return this;
    }

    public override sealed IArea GetSpreadMeasure()
    {
        return Area;
    }

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
