namespace CsabaDu.FooVaria.PlaneShapes.Factories.Implementations;

public sealed class RectangleFactory : PlaneShapeFactory, IRectangleFactory
{
    #region Constructors
    public RectangleFactory(IBulkSurfaceFactory bulkSpreadFactory, ICircleFactory tangentShapeFactory) : base(bulkSpreadFactory, tangentShapeFactory)
    {
    }
    #endregion

    #region Public methods
    public override IPlaneShape? CreateShape(params IShapeComponent[] shapeComponents)
    {
        return CreatePlaneShape(this, shapeComponents);
    }

    public IRectangle Create(IExtent length, IExtent width)
    {
        return new Rectangle(this, length, width);
    }

    public IRectangle CreateNew(IRectangle other)
    {
        return new Rectangle(other);
    }

    public ICircle CreateInnerTangentShape(IRectangle rectangle, ComparisonCode comparisonCode)
    {
        IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetComparedShapeExtent(comparisonCode);

        return CreateTangentShape(this, GetRadius(diagonal));
    }

    public ICircle CreateInnerTangentShape(IRectangle rectangle)
    {
        return CreateInnerTangentShape(rectangle, ComparisonCode.Less);
    }

    public ICircle CreateOuterTangentShape(IRectangle rectangle)
    {
        IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetDiagonal();

        return CreateTangentShape(this, GetRadius(diagonal));
    }

    public ICircle CreateTangentShape(IRectangle rectangle, SideCode sideCode)
    {
        return CreateTangentShape(this, rectangle, sideCode);
    }

    #region Override methods
    public override ICircleFactory GetTangentShapeFactory()
    {
        return (ICircleFactory)TangentShapeFactory;
    }
    #endregion
    #endregion

    #region Private methods
    private static IExtent GetRadius(IExtent diagonal)
    {
        return (IExtent)diagonal.Divide(2);
    }
    #endregion
}
