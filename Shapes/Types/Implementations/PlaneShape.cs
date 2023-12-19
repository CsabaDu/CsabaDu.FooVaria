namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

internal abstract class PlaneShape : Shape, IPlaneShape
{
    #region Constructor
    private protected PlaneShape(IPlaneShape other) : base(other)
    {
        Area = other.Area;
    }

    private protected PlaneShape(IPlaneShapeFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.AreaUnit, shapeExtents)
    {
        Area = GetArea(shapeExtents, nameof(shapeExtents));
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
    public override sealed IArea GetSpreadMeasure()
    {
        return Area;
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    private IArea GetArea(object arg, string paramName)
    {
        if (arg is IBaseShape baseShape) // Kell?
        {
            return getArea(getBaseShape());
        }
        else if (arg is IExtent[] shapeExtents)
        {
            return getArea(getSpread(shapeExtents));
        }
        else
        {
            throw new InvalidOperationException(null);
        }

        #region Local methods
        IArea getArea(IBaseSpread baseSpread)
        {
            return (IArea)baseSpread.GetSpreadMeasure();
        }

        IBaseShape getBaseShape()
        {
            Validate(baseShape, paramName);

            return baseShape;
        }

        ISpread getSpread(IExtent[] shapeExtents)
        {
            return GetSpreadFactory().Create(shapeExtents);
        }
        #endregion
    }
    #endregion
}
