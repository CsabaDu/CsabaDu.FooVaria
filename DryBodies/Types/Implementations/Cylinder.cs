namespace CsabaDu.FooVaria.DryBodies.Types.Implementations;

internal sealed class Cylinder : DryBody<ICylinder, ICircle>, ICylinder
{
    #region Constructors
    internal Cylinder(ICylinder other) : base(other)
    {
    }

    internal Cylinder(ICylinderFactory factory, IExtent radius, IExtent height) : base(factory, radius, height)
    {
    }

    internal Cylinder(ICylinderFactory factory, ICircle baseFace, IExtent height) : base(factory, baseFace, height)
    {
    }
    #endregion

    #region Properties
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
        return GetFactory().CreateInnerTangentShape(this, innerTangentRectangleSide);
    }

    public ICuboid GetInnerTangentShape()
    {
        return GetFactory().CreateInnerTangentShape(this);
    }

    public ICuboid GetOuterTangentShape()
    {
        return GetFactory().CreateOuterTangentShape(this);
    }

    public override ICylinder GetNew(ICylinder other)
    {
        return GetFactory().CreateNew(other);
    }

    public IExtent GetRadius()
    {
        return BaseFace.Radius;
    }

    public IExtent GetRadius(ExtentUnit extentUnit)
    {
        return GetRadius().GetMeasure(extentUnit);
    }

    public IBaseShape GetTangentShape(SideCode sideCode)
    {
        return GetFactory().CreateTangentShape(this, sideCode);
    }

    public IRectangle GetVerticalProjection()
    {
        return GetFactory().CreateVerticalProjection(this);
    }

    public ICylinder GetCylinder(IExtent radius, IExtent height)
    {
        return GetFactory().Create(radius, height);
    }

    #region Override methods
    public override ICircleFactory GetBaseFaceFactory()
    {
        return (ICircleFactory)base.GetBaseFaceFactory();
    }

    public override ICylinderFactory GetFactory()
    {
        return (ICylinderFactory)Factory;
    }

    public override IPlaneShape GetProjection(ShapeExtentCode perpendicular)
    {
        return GetFactory().CreateProjection(this, perpendicular)!;
    }

    public override ICuboidFactory GetTangentShapeFactory()
    {
        return (ICuboidFactory)base.GetTangentShapeFactory();
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        IRectangle verticalProjection = GetVerticalProjection();

        return verticalProjection.GetDiagonal(extentUnit);
    }
    #endregion
    #endregion
}
