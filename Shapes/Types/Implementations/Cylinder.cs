namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

internal sealed class Cylinder : DryBody<ICylinder, ICircle>, ICylinder
{
    internal Cylinder(ICylinder other) : base(other)
    {
    }

    internal Cylinder(ICylinderFactory factory, IExtent radius, IExtent height) : base(factory, radius, height)
    {
    }

    internal Cylinder(ICylinderFactory factory, ICircle baseFace, IExtent height) : base(factory, baseFace, height)
    {
    }


    public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
    {
        ShapeExtentTypeCode.Radius => GetRadius(),
        ShapeExtentTypeCode.Height => Height,

        _ => null,
    };

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

    public override ICircleFactory GetBaseFaceFactory()
    {
        return (ICircleFactory)base.GetBaseFaceFactory();
    }

    public override IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular)
    {
        return GetFactory().CreateProjection(this, perpendicular);
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
        return GetFactory().CreateTangentShape(this, sideCode);
    }

    public override ICuboidFactory GetTangentShapeFactory()
    {
        return (ICuboidFactory)GetFactory().TangentShapeFactory;
    }
    public IRectangle GetVerticalProjection()
    {
        return GetFactory().CreateVerticalProjection(this);
    }

    public override ICylinderFactory GetFactory()
    {
        return (ICylinderFactory)Factory;
    }

    public ICylinder GetCylinder(IExtent radius, IExtent height)
    {
        return GetFactory().Create(radius, height);
    }
}
