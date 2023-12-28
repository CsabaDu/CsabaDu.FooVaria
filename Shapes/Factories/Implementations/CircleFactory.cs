namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public sealed class CircleFactory : PlaneShapeFactory, ICircleFactory
{
    public CircleFactory(IBulkSurfaceFactory spreadFactory, IRectangleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
    {
    }

    public ICircle Create(IExtent radius)
    {
        return new Circle(this, radius);
    }

    public ICircle CreateNew(ICircle other)
    {
        return new Circle(other);
    }

    public override ICircle? CreateBaseShape(params IShapeComponent[] shapeComponents)
    {
        int count = GetShapeComponentsCount(shapeComponents);

        if (count == 1)
        {
            if (GetShapeExtent(shapeComponents[0]) is IExtent radius) return Create(radius);

            if (shapeComponents[0] is ICircle circle) return CreateNew(circle);
        }

        return null;
    }

    public IRectangle CreateInnerTangentShape(ICircle circle, IExtent tangentRectangleSide)
    {
        IExtent otherSide = ShapeExtents.GetInnerTangentRectangleSide(circle, tangentRectangleSide);

        return CreateTangentShape(this, tangentRectangleSide, otherSide);
    }

    public IRectangle CreateInnerTangentShape(ICircle circle)
    {
        IExtent side = ShapeExtents.GetInnerTangentRectangleSide(circle);

        return CreateTangentShape(this, side, side);
    }

    public IRectangle CreateOuterTangentShape(ICircle circle)
    {
        return CreateTangentShape(this, circle);
    }

    public IRectangle CreateTangentShape(ICircle circle, SideCode sideCode)
    {
        return CreateTangentShape(this, circle, sideCode);
    }

    public override IRectangleFactory GetTangentShapeFactory()
    {
        return (IRectangleFactory)TangentShapeFactory;
    }
}
