namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

internal sealed class Circle : PlaneShape, ICircle
{
    internal Circle(ICircle other) : base(other)
    {
        Radius = other.Radius;
    }

    internal Circle(ICircleFactory factory, IExtent radius) : base(factory, radius)
    {
        Radius = radius;
    }


    public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
    {
        ShapeExtentTypeCode.Radius => Radius,

        _ => null,
    };

    public IExtent Radius { get; init; }

    public override IEnumerable<IExtent> GetDimensions()
    {
        for (int i = 0; i < GetShapeExtentCount(); i++)
        {
            yield return GetDiagonal();
        }
    }

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

    public override ICircleFactory GetFactory()
    {
        return (ICircleFactory)Factory;
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

    public override IRectangleFactory GetTangentShapeFactory()
    {
        return (IRectangleFactory)GetFactory().TangentShapeFactory;
    }

    public ICircle GetCircle(IExtent radius)
    {
        return GetFactory().Create(radius);
    }
}
