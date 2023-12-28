namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public sealed class RectangleFactory : PlaneShapeFactory, IRectangleFactory
{
    #region Constructors
    public RectangleFactory(IBulkSurfaceFactory spreadFactory, ICircleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
    {
    }
    #endregion

    #region Public methods
    public override IRectangle? CreateBaseShape(params IShapeComponent[] shapeComponents)
    {
        int count = GetShapeComponentsCount(shapeComponents);

        return count switch
        {
            1 => createRectangleFrom1Param(),
            2 => createRectangleFrom2Params(),

            _ => null,

        };

        #region Local methods
        IRectangle? createRectangleFrom1Param()
        {
            return shapeComponents[0] is IRectangle rectangle ?
                CreateNew(rectangle)
                : null;
        }

        IRectangle? createRectangleFrom2Params()
        {
            IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

            return shapeExtents != null ?
                Create(shapeExtents.First(), shapeExtents.Last())
                : null;
        }
        #endregion
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
