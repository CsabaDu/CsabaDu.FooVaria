namespace CsabaDu.FooVaria.SimpleShapes.Types.Implementations;

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

    public ICuboid GetCuboid(IExtent length, IExtent width, IExtent height)
    {
        return GetFactory().Create(length, width, height);
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

    public ISimpleShape GetTangentShape(SideCode sideCode)
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
        return (ICuboid)GetSimpleShape(GetSortedDimensions().ToArray());
    }

    public ICuboid RotateTo(IDryBody other)
    {
        if (NullChecked(other, nameof(other)) is ICylinder cylinder)
        {
            other = cylinder.GetOuterTangentShape();
        }

        if (other is not ICuboid cuboid) throw exception();

        IExtent longest = cuboid.GetComparedShapeExtent(ComparisonCode.Greater);
        IExtent shortest = cuboid.GetComparedShapeExtent(ComparisonCode.Less);

        if (!cuboid.TryGetShapeExtentTypeCode(longest, out ShapeExtentTypeCode? longestCode)
            || !cuboid.TryGetShapeExtentTypeCode(shortest, out ShapeExtentTypeCode? shortestCode))
        {
            throw exception();
        }

        longest = GetComparedShapeExtent(ComparisonCode.Greater);
        shortest = GetComparedShapeExtent(ComparisonCode.Less);
        IExtent medium = GetComparedShapeExtent(null);

        return longestCode!.Value switch
        {
            ShapeExtentTypeCode.Length => shortestCode!.Value switch
            {
                ShapeExtentTypeCode.Width => GetCuboid(longest, shortest, medium),
                ShapeExtentTypeCode.Height => GetCuboid(longest, medium, shortest),

                _ => throw exception(),
            },
            ShapeExtentTypeCode.Width => shortestCode!.Value switch
            {
                ShapeExtentTypeCode.Length => GetCuboid(shortest, longest, medium),
                ShapeExtentTypeCode.Height => GetCuboid(medium, longest, shortest),

                _ => throw exception(),
            },
            ShapeExtentTypeCode.Height => shortestCode!.Value switch
            {
                ShapeExtentTypeCode.Length => RotateSpatially(),
                ShapeExtentTypeCode.Width => GetCuboid(medium, shortest, longest),

                _ => throw exception(),
            },

            _ => throw exception(),
        };

        #region Local methods
        InvalidOperationException exception()
        {
            throw new InvalidOperationException(null);
        }
        #endregion
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
