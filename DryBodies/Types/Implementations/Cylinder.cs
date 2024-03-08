namespace CsabaDu.FooVaria.DryBodies.Types.Implementations;

internal sealed class Cylinder : DryBody<ICylinder, ICircle>, ICylinder
{
    #region Constructors
    internal Cylinder(ICylinder other) : base(other)
    {
        Factory = other.Factory;
    }

    internal Cylinder(ICylinderFactory factory, IExtent radius, IExtent height) : base(factory, radius, height)
    {
        Factory = factory;
    }

    internal Cylinder(ICylinderFactory factory, ICircle baseFace, IExtent height) : base(factory, baseFace, height)
    {
        Factory = factory;
    }
    #endregion

    #region Properties
    public ICylinderFactory Factory { get; init; }

    #region Override properties
    public override IExtent? this[ShapeExtentCode shapeExtentCode] => shapeExtentCode switch
    {
        ShapeExtentCode.Radius => GetRadius(),
        ShapeExtentCode.Height => Height,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    public ICuboid GetInnerTangentShape(IExtent innerTangentRectangleSide)
    {
        return Factory.CreateInnerTangentShape(this, innerTangentRectangleSide);
    }

    public ICuboid GetInnerTangentShape()
    {
        return Factory.CreateInnerTangentShape(this);
    }

    public ICuboid GetOuterTangentShape()
    {
        return Factory.CreateOuterTangentShape(this);
    }

    public override ICylinder GetNew(ICylinder other)
    {
        return Factory.CreateNew(other);
    }

    public IExtent GetRadius()
    {
        return BaseFace.Radius;
    }

    public IExtent GetRadius(ExtentUnit extentUnit)
    {
        return GetRadius().GetMeasure(extentUnit);
    }

    public IShape GetTangentShape(SideCode sideCode)
    {
        return Factory.CreateTangentShape(this, sideCode);
    }

    public IRectangle GetVerticalProjection()
    {
        return Factory.CreateVerticalProjection(this);
    }

    public ICylinder GetCylinder(IExtent radius, IExtent height)
    {
        return Factory.Create(radius, height);
    }

    #region Override methods
    public override ICircleFactory GetBaseFaceFactory()
    {
        return (ICircleFactory)Factory.GetBaseFaceFactory();
    }

    public override ICylinderFactory GetFactory()
    {
        return Factory;
    }

    public override IPlaneShape GetProjection(ShapeExtentCode perpendicular)
    {
        return Factory.CreateProjection(this, perpendicular)!;
    }

    public override ICuboidFactory GetTangentShapeFactory()
    {
        return Factory.TangentShapeFactory;
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        IRectangle verticalProjection = GetVerticalProjection();

        return verticalProjection.GetDiagonal(extentUnit);
    }
    #endregion
    #endregion
}
