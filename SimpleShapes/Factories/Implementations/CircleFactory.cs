namespace CsabaDu.FooVaria.SimpleShapes.Factories.Implementations;

public sealed class CircleFactory : PlaneShapeFactory, ICircleFactory
{
    #region Constructors
    public CircleFactory(IBulkSurfaceFactory spreadFactory, IRectangleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
    {
    }
    #endregion

    #region Public methods
    public ICircle Create(IExtent radius)
    {
        return new Circle(this, radius);
    }

    public ICircle CreateNew(ICircle other)
    {
        return new Circle(other);
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

    #region Override methods
    public override IPlaneShape? CreateBaseShape(params IShapeComponent[] shapeComponents)
    {
        return CreatePlaneShape(GetTangentShapeFactory(), shapeComponents);
    }

    public override IRectangleFactory GetTangentShapeFactory()
    {
        return (IRectangleFactory)TangentShapeFactory;
    }
    #endregion
    #endregion
}
