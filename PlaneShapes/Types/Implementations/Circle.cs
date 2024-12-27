namespace CsabaDu.FooVaria.PlaneShapes.Types.Implementations;

internal sealed class Circle : PlaneShape, ICircle
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Circle"/> class by copying another circle.
    /// </summary>
    /// <param name="other">The other circle to copy.</param>
    internal Circle(ICircle other) : base(other)
    {
        Radius = other.Radius;
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle"/> class using a factory and radius.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="radius">The radius of the circle.</param>
    internal Circle(ICircleFactory factory, IExtent radius) : base(factory, radius)
    {
        Radius = radius;
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the radius of the circle.
    /// </summary>
    public IExtent Radius { get; init; }

    /// <summary>
    /// Gets the factory used to create instances of the circle.
    /// </summary>
    public ICircleFactory Factory { get; init; }

    #region Override properties
    /// <summary>
    /// Gets the extent of the shape based on the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The extent of the shape.</returns>
    public override IExtent? this[ShapeExtentCode shapeExtentCode] => shapeExtentCode switch
    {
        ShapeExtentCode.Radius => Radius,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    /// <summary>
    /// Gets the inner tangent rectangle shape with the specified side length.
    /// </summary>
    /// <param name="innerTangentRectangleSide">The side length of the inner tangent rectangle.</param>
    /// <returns>The inner tangent rectangle shape.</returns>
    public IRectangle GetInnerTangentShape(IExtent innerTangentRectangleSide)
    {
        return GetFactory().CreateInnerTangentShape(this, innerTangentRectangleSide);
    }

    /// <summary>
    /// Gets the inner tangent rectangle shape.
    /// </summary>
    /// <returns>The inner tangent rectangle shape.</returns>
    public IRectangle GetInnerTangentShape()
    {
        return GetFactory().CreateInnerTangentShape(this);
    }

    /// <summary>
    /// Gets the outer tangent rectangle shape.
    /// </summary>
    /// <returns>The outer tangent rectangle shape.</returns>
    public IRectangle GetOuterTangentShape()
    {
        return GetFactory().CreateOuterTangentShape(this);
    }

    /// <summary>
    /// Creates a new circle by copying another circle.
    /// </summary>
    /// <param name="other">The other circle to copy.</param>
    /// <returns>The new circle.</returns>
    public ICircle GetNew(ICircle other)
    {
        return GetFactory().CreateNew(other);
    }

    /// <summary>
    /// Gets the radius of the circle.
    /// </summary>
    /// <returns>The radius of the circle.</returns>
    public IExtent GetRadius()
    {
        return Radius;
    }

    /// <summary>
    /// Gets the radius of the circle in the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The radius of the circle in the specified extent unit.</returns>
    public IExtent GetRadius(ExtentUnit extentUnit)
    {
        return Radius.GetMeasure(extentUnit);
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
    /// Creates a new circle with the specified radius.
    /// </summary>
    /// <param name="radius">The radius of the new circle.</param>
    /// <returns>The new circle.</returns>
    public ICircle GetCircle(IExtent radius)
    {
        return GetFactory().Create(radius);
    }

    #region Override methods
    /// <summary>
    /// Gets the factory used to create instances of the circle.
    /// </summary>
    /// <returns>The factory.</returns>
    public override ICircleFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets the factory used to create tangent shapes.
    /// </summary>
    /// <returns>The tangent shape factory.</returns>
    public override IRectangleFactory GetTangentShapeFactory()
    {
        return GetFactory().TangentShapeFactory;
    }

    /// <summary>
    /// Gets the diagonal of the circle in the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The diagonal of the circle.</returns>
    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        ValidateMeasureUnitByDefinition(extentUnit, nameof(extentUnit));

        IMeasure radius = Radius;
        decimal quantity = radius.GetDefaultQuantity() * 2;
        quantity = IsDefaultMeasureUnit(extentUnit) ?
            quantity
            : quantity / GetExchangeRate(extentUnit, nameof(extentUnit));

        return (IExtent)radius.GetBaseMeasure(extentUnit, quantity);
    }
    #endregion
    #endregion
}
