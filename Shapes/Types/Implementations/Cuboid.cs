namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

internal sealed class Cuboid : DryBody<ICuboid, IRectangle>, ICuboid
{
    #region Constructors
    internal Cuboid(ICuboid other) : base(other)
    {
    }

    internal Cuboid(ICuboidFactory factory, IExtent length, IExtent width, IExtent height) : base(factory, length, width, height)
    {
    }

    internal Cuboid(ICuboidFactory factory, IRectangle baseFace, IExtent height) : base(factory, baseFace, height)
    {
    }
    #endregion

    #region Properties
    #region Override properties
    public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
    {
        ShapeExtentTypeCode.Length => GetLength(),
        ShapeExtentTypeCode.Width => GetWidth(),
        ShapeExtentTypeCode.Height => Height,

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    public IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode)
    {
        IEnumerable<IExtent> shapeExtents = GetSortedDimensions();

        return comparisonCode switch
        {
            null => shapeExtents.ElementAt(1),
            ComparisonCode.Greater => shapeExtents.Last(),
            ComparisonCode.Less => shapeExtents.First(),

            _ => throw InvalidComparisonCodeEnumArgumentException(comparisonCode!.Value),
        };
    }

    public ICylinder GetInnerTangentShape(ComparisonCode comparisonCode)
    {
        ICircle baseFace = BaseFace.GetInnerTangentShape(comparisonCode);

        return GetTangentShapeFactory().Create(baseFace, Height);
    }

    public ICylinder GetInnerTangentShape()
    {
        return GetInnerTangentShape(ComparisonCode.Less);
    }

    public IExtent GetLength()
    {
        return BaseFace.Length;
    }

    public IExtent GetLength(ExtentUnit extentUnit)
    {
        return GetLength().GetMeasure(extentUnit);
    }

    public ICylinder GetOuterTangentShape()
    {
        return GetFactory().CreateOuterTangentShape(this);
    }

    public ICuboid GetNew(ICuboid other)
    {
        return GetFactory().CreateNew(other);
    }

    public IShape GetTangentShape(SideCode sideCode)
    {
        return GetFactory().CreateTangentShape(this, sideCode);
    }

    public IRectangle GetVerticalProjection(ComparisonCode comparisonCode)
    {
        return GetFactory().CreateVerticalProjection(this, comparisonCode);
    }

    public IExtent GetWidth()
    {
        return BaseFace.Width;
    }

    public IExtent GetWidth(ExtentUnit extentUnit)
    {
        return GetWidth().GetMeasure(extentUnit);
    }

    public ICuboid RotateHorizontally()
    {
        IRectangle baseFace = BaseFace.RotateHorizontally();

        return GetDryBody(baseFace, Height);
    }

    public ICuboid RotateSpatially()
    {
        return (ICuboid)GetShape(GetSortedDimensions().ToArray());
    }

    public ICuboid GetCuboid(IExtent length, IExtent width, IExtent height)
    {
        return GetFactory().Create(length, width, height);
    }

    #region Override methods
    public override IRectangleFactory GetBaseFaceFactory()
    {
        return (IRectangleFactory)base.GetBaseFaceFactory();
    }

    public override IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular)
    {
        return GetBaseFaceFactory().CreateProjection(this, perpendicular)!;
    }

    public override ICylinderFactory GetTangentShapeFactory()
    {
        return (ICylinderFactory)base.GetTangentShapeFactory();
    }

    public override ICuboidFactory GetFactory()
    {
        return (ICuboidFactory)Factory;
    }
    #endregion
    #endregion
}
