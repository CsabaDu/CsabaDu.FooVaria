namespace CsabaDu.FooVaria.DryBodies.Types.Implementations;

internal sealed class Cylinder : DryBody<ICylinder, ICircle>, ICylinder
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Cylinder"/> class by copying another cylinder.
    /// </summary>
    /// <param name="other">The other cylinder to copy.</param>
    internal Cylinder(ICylinder other) : base(other)
    {
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cylinder"/> class using a factory, radius, and height.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="radius">The radius of the cylinder.</param>
    /// <param name="height">The height of the cylinder.</param>
    internal Cylinder(ICylinderFactory factory, IExtent radius, IExtent height) : base(factory, radius, height)
    {
        Factory = factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cylinder"/> class using a factory, base face, and height.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="baseFace">The base face of the cylinder.</param>
    /// <param name="height">The height of the cylinder.</param>
    internal Cylinder(ICylinderFactory factory, ICircle baseFace, IExtent height) : base(factory, baseFace, height)
    {
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the factory used to create instances of the cylinder.
    /// </summary>
    public ICylinderFactory Factory { get; init; }

    #region Override properties
    /// <summary>
    /// Gets the extent of the cylinder based on the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The extent of the cylinder.</returns>
    public override IExtent? this[ShapeExtentCode shapeExtentCode] => shapeExtentCode switch
    {
        ShapeExtentCode.Radius => GetRadius(),
        ShapeExtentCode.Height => Height,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    /// <summary>
    /// Gets the inner tangent shape of the cylinder with the specified inner tangent rectangle side.
    /// </summary>
    /// <param name="innerTangentRectangleSide">The inner tangent rectangle side.</param>
    /// <returns>The inner tangent shape of the cylinder.</returns>
    public ICuboid GetInnerTangentShape(IExtent innerTangentRectangleSide)
    {
        return Factory.CreateInnerTangentShape(this, innerTangentRectangleSide);
    }

    /// <summary>
    /// Gets the inner tangent shape of the cylinder.
    /// </summary>
    /// <returns>The inner tangent shape of the cylinder.</returns>
    public ICuboid GetInnerTangentShape()
    {
        return Factory.CreateInnerTangentShape(this);
    }

    /// <summary>
    /// Gets the outer tangent shape of the cylinder.
    /// </summary>
    /// <returns>The outer tangent shape of the cylinder.</returns>
    public ICuboid GetOuterTangentShape()
    {
        return Factory.CreateOuterTangentShape(this);
    }

    /// <summary>
    /// Gets a new instance of the cylinder by copying another cylinder.
    /// </summary>
    /// <param name="other">The other cylinder to copy.</param>
    /// <returns>A new instance of the cylinder.</returns>
    public override ICylinder GetNew(ICylinder other)
    {
        return Factory.CreateNew(other);
    }

    /// <summary>
    /// Gets the radius of the cylinder.
    /// </summary>
    /// <returns>The radius of the cylinder.</returns>
    public IExtent GetRadius()
    {
        return BaseFace.Radius;
    }

    /// <summary>
    /// Gets the radius of the cylinder with the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The radius of the cylinder.</returns>
    public IExtent GetRadius(ExtentUnit extentUnit)
    {
        return GetRadius().GetMeasure(extentUnit);
    }

    /// <summary>
    /// Gets the tangent shape of the cylinder based on the specified side code.
    /// </summary>
    /// <param name="sideCode">The side code.</param>
    /// <returns>The tangent shape of the cylinder.</returns>
    public IShape GetTangentShape(SideCode sideCode)
    {
        return Factory.CreateTangentShape(this, sideCode);
    }

    /// <summary>
    /// Gets the vertical projection of the cylinder.
    /// </summary>
    /// <returns>The vertical projection of the cylinder.</returns>
    public IRectangle GetVerticalProjection()
    {
        return Factory.CreateVerticalProjection(this);
    }

    /// <summary>
    /// Gets a new instance of the cylinder with the specified radius and height.
    /// </summary>
    /// <param name="radius">The radius of the cylinder.</param>
    /// <param name="height">The height of the cylinder.</param>
    /// <returns>A new instance of the cylinder.</returns>
    public ICylinder GetCylinder(IExtent radius, IExtent height)
    {
        return Factory.Create(radius, height);
    }

    #region Override methods
    /// <summary>
    /// Gets the base face factory of the cylinder.
    /// </summary>
    /// <returns>The base face factory of the cylinder.</returns>
    public override ICircleFactory GetBaseFaceFactory()
    {
        return (ICircleFactory)Factory.GetBaseFaceFactory();
    }

    /// <summary>
    /// Gets the factory used to create instances of the cylinder.
    /// </summary>
    /// <returns>The factory used to create instances of the cylinder.</returns>
    public override ICylinderFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets the projection of the cylinder for the specified shape extent code.
    /// </summary>
    /// <param name="perpendicular">The shape extent code.</param>
    /// <returns>The projection of the cylinder.</returns>
    public override IPlaneShape GetProjection(ShapeExtentCode perpendicular)
    {
        return Factory.CreateProjection(this, perpendicular)!;
    }

    /// <summary>
    /// Gets the tangent shape factory of the cylinder.
    /// </summary>
    /// <returns>The tangent shape factory of the cylinder.</returns>
    public override ICuboidFactory GetTangentShapeFactory()
    {
        return Factory.TangentShapeFactory;
    }

    /// <summary>
    /// Gets the diagonal of the cylinder with the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The diagonal of the cylinder.</returns>
    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        IRectangle verticalProjection = GetVerticalProjection();

        return verticalProjection.GetDiagonal(extentUnit);
    }
    #endregion
    #endregion
}
