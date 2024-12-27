namespace CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

/// <summary>
/// Represents a rectangle shape.
/// </summary>
internal sealed class Rectangle : PlaneShape, IRectangle
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> class by copying another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle to copy.</param>
    internal Rectangle(IRectangle other) : base(other)
    {
        Length = other.Length;
        Width = other.Width;
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> class using a factory and shape extents.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="length">The length of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    internal Rectangle(IRectangleFactory factory, IExtent length, IExtent width) : base(factory, length, width)
    {
        Length = length;
        Width = width;
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the length of the rectangle.
    /// </summary>
    public IExtent Length { get; init; }

    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    public IExtent Width { get; init; }

    /// <summary>
    /// Gets the factory used to create the rectangle.
    /// </summary>
    public IRectangleFactory Factory { get; init; }

    #region Override properties
    /// <summary>
    /// Gets the shape extent based on the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The corresponding shape extent, or null if the code is not recognized.</returns>
    public override IExtent? this[ShapeExtentCode shapeExtentCode] => shapeExtentCode switch
    {
        ShapeExtentCode.Length => Length,
        ShapeExtentCode.Width => Width,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    /// <summary>
    /// Gets the shape extent based on the specified comparison code.
    /// </summary>
    /// <param name="comparisonCode">The comparison code.</param>
    /// <returns>The corresponding shape extent.</returns>
    /// <exception cref="ArgumentException">Thrown when the comparison code is invalid.</exception>
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

    /// <summary>
    /// Gets the inner tangent circle based on the specified comparison code.
    /// </summary>
    /// <param name="comparisonCode">The comparison code.</param>
    /// <returns>The inner tangent circle.</returns>
    public ICircle GetInnerTangentShape(ComparisonCode comparisonCode)
    {
        return GetFactory().CreateInnerTangentShape(this, comparisonCode);
    }

    /// <summary>
    /// Gets the inner tangent circle.
    /// </summary>
    /// <returns>The inner tangent circle.</returns>
    public ICircle GetInnerTangentShape()
    {
        return GetFactory().CreateInnerTangentShape(this);
    }

    /// <summary>
    /// Gets the length of the rectangle.
    /// </summary>
    /// <returns>The length of the rectangle.</returns>
    public IExtent GetLength()
    {
        return Length;
    }

    /// <summary>
    /// Gets the length of the rectangle in the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The length of the rectangle in the specified extent unit.</returns>
    public IExtent GetLength(ExtentUnit extentUnit)
    {
        return Length.GetMeasure(extentUnit);
    }

    /// <summary>
    /// Gets the outer tangent circle.
    /// </summary>
    /// <returns>The outer tangent circle.</returns>
    public ICircle GetOuterTangentShape()
    {
        return GetFactory().CreateOuterTangentShape(this);
    }

    /// <summary>
    /// Creates a new rectangle by copying another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle to copy.</param>
    /// <returns>The new rectangle.</returns>
    public IRectangle GetNew(IRectangle other)
    {
        return GetFactory().CreateNew(other);
    }

    /// <summary>
    /// Gets the tangent shape based on the specified side code.
    /// </summary>
    /// <param name="sideCode">The side code.</param>
    /// <returns>The tangent shape.</returns>
    public IShape GetTangentShape(SideCode sideCode)
    {
        return GetFactory().CreateTangentShape(this, sideCode);
    }

    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    /// <returns>The width of the rectangle.</returns>
    public IExtent GetWidth()
    {
        return Width;
    }

    /// <summary>
    /// Gets the width of the rectangle in the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The width of the rectangle in the specified extent unit.</returns>
    public IExtent GetWidth(ExtentUnit extentUnit)
    {
        return Width.GetMeasure(extentUnit);
    }

    /// <summary>
    /// Rotates the rectangle horizontally.
    /// </summary>
    /// <returns>The rotated rectangle.</returns>
    public IRectangle RotateHorizontally()
    {
        return (IRectangle)GetShape(GetSortedDimensions().ToArray())!;
    }

    /// <summary>
    /// Creates a new rectangle with the specified length and width.
    /// </summary>
    /// <param name="length">The length of the new rectangle.</param>
    /// <param name="width">The width of the new rectangle.</param>
    /// <returns>The new rectangle.</returns>
    public IRectangle GetRectangle(IExtent length, IExtent width)
    {
        return GetFactory().Create(length, width);
    }

    #region Override methods
    /// <summary>
    /// Gets the factory used to create the rectangle.
    /// </summary>
    /// <returns>The rectangle factory.</returns>
    public override IRectangleFactory GetFactory()
    {
        return (IRectangleFactory)Factory;
    }

    /// <summary>
    /// Gets the factory used to create tangent shapes.
    /// </summary>
    /// <returns>The tangent shape factory.</returns>
    public override ICircleFactory GetTangentShapeFactory()
    {
        return GetFactory().TangentShapeFactory;
    }

    /// <summary>
    /// Gets the diagonal of the rectangle in the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The diagonal of the rectangle in the specified extent unit.</returns>
    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return GetRectangularShapeDiagonal(this, extentUnit);
    }
    #endregion
    #endregion
}
