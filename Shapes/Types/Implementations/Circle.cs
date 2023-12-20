namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

internal sealed class Circle : PlaneShape, ICircle
{
    #region Constructors
    internal Circle(ICircle other) : base(other)
    {
        Radius = other.Radius;
    }

    internal Circle(ICircleFactory factory, IExtent radius) : base(factory, radius)
    {
        Radius = radius;
    }
    #endregion

    #region Properties
    public IExtent Radius { get; init; }

    #region Override properties
    public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
    {
        ShapeExtentTypeCode.Radius => Radius,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    public IRectangle GetInnerTangentShape(IExtent innerTangentRectangleSide)
    {
        return GetFactory().CreateInnerTangentShape(this, innerTangentRectangleSide);
    }

    public IRectangle GetInnerTangentShape()
    {
        return GetFactory().CreateInnerTangentShape(this);
    }

    public IRectangle GetOuterTangentShape()
    {
        return GetFactory().CreateOuterTangentShape(this);
    }

    public IExtent GetRadius()
    {
        return Radius;
    }

    public IExtent GetRadius(ExtentUnit extentUnit)
    {
        return Radius.GetMeasure(extentUnit);
    }

    public IShape GetTangentShape(SideCode sideCode)
    {
        return GetFactory().CreateTangentShape(this, sideCode);
    }

    public ICircle GetCircle(IExtent radius)
    {
        return GetFactory().Create(radius);
    }

    #region Override methods
    public override IEnumerable<IExtent> GetDimensions()
    {
        for (int i = 0; i < GetShapeComponentCount(); i++)
        {
            yield return GetDiagonal();
        }
    }

    public override ICircleFactory GetFactory()
    {
        return (ICircleFactory)Factory;
    }

    public override IRectangleFactory GetTangentShapeFactory()
    {
        return (IRectangleFactory)GetFactory().TangentShapeFactory;
    }

    public ICircle GetNew(ICircle other)
    {
        throw new NotImplementedException();
    }
    #endregion
    #endregion
}
