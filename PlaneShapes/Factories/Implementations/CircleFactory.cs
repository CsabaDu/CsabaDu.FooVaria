using CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

namespace CsabaDu.FooVaria.PlaneShapes.Factories.Implementations;

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
        IExtent diagonal = NullChecked(circle, nameof(circle)).GetDiagonal();

        if (tangentRectangleSide.CompareTo(diagonal) <= 0)
        {
            throw QuantityArgumentOutOfRangeException(nameof(tangentRectangleSide), tangentRectangleSide.GetQuantity());
        }

        decimal quantitySquare = GetDefaultQuantitySquare(tangentRectangleSide);
        quantitySquare = GetDefaultQuantitySquare(diagonal) - quantitySquare;
        IExtent otherSide = GetInnerTangentRectangleSide(diagonal, quantitySquare);

        return CreateTangentShape(this, tangentRectangleSide, otherSide);
    }

    public IRectangle CreateInnerTangentShape(ICircle circle)
    {
        IExtent diagonal = NullChecked(circle, nameof(circle)).GetDiagonal();
        decimal quantitySquare = GetDefaultQuantitySquare(diagonal) / 2;
        IExtent side = GetInnerTangentRectangleSide(diagonal, quantitySquare);

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
    public override IPlaneShape? CreateShape(params ISimpleShapeComponent[] shapeComponents)
    {
        return CreatePlaneShape(GetTangentShapeFactory(), shapeComponents);
    }

    public override IRectangleFactory GetTangentShapeFactory()
    {
        return (IRectangleFactory)TangentShapeFactory;
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static IExtent GetInnerTangentRectangleSide(IExtent diagonal, decimal quantitySquare)
    {
        double quantity = decimal.ToDouble(quantitySquare);
        quantity = Math.Sqrt(quantity);

        return diagonal.GetMeasure(default, quantity);
    }
    #endregion
    #endregion
}
