namespace CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

internal abstract class PlaneShape : SimpleShape, IPlaneShape
{
    #region Constructor
    private protected PlaneShape(IPlaneShape other) : base(other)
    {
        Area = other.Area;
    }

    private protected PlaneShape(IPlaneShapeFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitCode.AreaUnit, shapeExtents)
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
    public override IPlaneShapeFactory GetFactory()
    {
        return (IPlaneShapeFactory)Factory;
    }

    #region Sealed methods
    public override sealed IBulkSurfaceFactory GetSpreadFactory()
    {
        return (IBulkSurfaceFactory)base.GetSpreadFactory();
    }

    public override sealed IArea GetSpreadMeasure()
    {
        return Area;
    }
    #endregion
    #endregion
    #endregion
}
