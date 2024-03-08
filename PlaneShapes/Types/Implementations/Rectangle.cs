namespace CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

internal sealed class Rectangle : PlaneShape, IRectangle
{
    #region Constructors
    internal Rectangle(IRectangle other) : base(other)
    {
        Length = other.Length;
        Width = other.Width;
        Factory = other.Factory;
    }

    internal Rectangle(IRectangleFactory factory, IExtent length, IExtent width) : base(factory, length, width)
    {
        Length = length;
        Width = width;
        Factory = factory;
    }
    #endregion

    #region Properties
    public IExtent Length { get; init; }
    public IExtent Width { get; init; }
    public IRectangleFactory Factory { get; init; }

    #region Override properties
    public override IExtent? this[ShapeExtentCode shapeExtentCode] => shapeExtentCode switch
    {
        ShapeExtentCode.Length => Length,
        ShapeExtentCode.Width => Width,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    public IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode)
    {
        _ = NullChecked(comparisonCode, nameof(comparisonCode));

        IEnumerable<IExtent> shapeExtents = GetSortedDimensions();

        return comparisonCode switch
        {
            ComparisonCode.Greater => shapeExtents.Last(),
            ComparisonCode.Less => shapeExtents.First(),

            _ => throw InvalidComparisonCodeEnumArgumentException(comparisonCode!.Value),
        };
    }

    public ICircle GetInnerTangentShape(ComparisonCode comparisonCode)
    {
        return GetFactory().CreateInnerTangentShape(this, comparisonCode);
    }

    public ICircle GetInnerTangentShape()
    {
        return GetFactory().CreateInnerTangentShape(this);
    }

    public IExtent GetLength()
    {
        return Length;
    }

    public IExtent GetLength(ExtentUnit extentUnit)
    {
        return Length.GetMeasure(extentUnit);
    }

    public ICircle GetOuterTangentShape()
    {
        return GetFactory().CreateOuterTangentShape(this);
    }

    public IRectangle GetNew(IRectangle other)
    {
        return GetFactory().CreateNew(other);
    }

    public IShape GetTangentShape(SideCode sideCode)
    {
        return GetFactory().CreateTangentShape(this, sideCode);
    }

    public IExtent GetWidth()
    {
        return Width;
    }

    public IExtent GetWidth(ExtentUnit extentUnit)
    {
        return Width.GetMeasure(extentUnit);
    }

    public IRectangle RotateHorizontally()
    {
        return (IRectangle)GetShape(GetSortedDimensions().ToArray())!;
    }

    public IRectangle GetRectangle(IExtent length, IExtent width)
    {
        return GetFactory().Create(length, width);
    }

    #region Override methods
    public override IRectangleFactory GetFactory()
    {
        return (IRectangleFactory)Factory;
    }

    public override ICircleFactory GetTangentShapeFactory()
    {
        return GetFactory().TangentShapeFactory;
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return GetRectangularShapeDiagonal(this, extentUnit);
    }
    #endregion
    #endregion
}
