﻿namespace CsabaDu.FooVaria.DryBodies.Types.Implementations;

internal sealed class Cuboid : DryBody<ICuboid, IRectangle>, ICuboid
{
    #region Constructors
    internal Cuboid(ICuboid other) : base(other)
    {
        Factory = other.Factory;
    }

    internal Cuboid(ICuboidFactory factory, IExtent length, IExtent width, IExtent height) : base(factory, length, width, height)
    {
        Factory = factory;
    }

    internal Cuboid(ICuboidFactory factory, IRectangle baseFace, IExtent height) : base(factory, baseFace, height)
    {
        Factory = factory;
    }
    #endregion

    #region Properties
    public ICuboidFactory Factory { get; init; }

    #region Override properties
    public override IExtent? this[ShapeExtentCode shapeExtentCode] => shapeExtentCode switch
    {
        ShapeExtentCode.Length => GetLength(),
        ShapeExtentCode.Width => GetWidth(),
        ShapeExtentCode.Height => Height,

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
        return Factory.Create(length, width, height);
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
        return Factory.CreateOuterTangentShape(this);
    }

    public override ICuboid GetNew(ICuboid other)
    {
        return Factory.CreateNew(other);
    }

    public IShape GetTangentShape(SideCode sideCode)
    {
        return Factory.CreateTangentShape(this, sideCode);
    }

    public IRectangle GetVerticalProjection(ComparisonCode comparisonCode)
    {
        return Factory.CreateVerticalProjection(this, comparisonCode);
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

        var (longest, shortest) = getComparedShapeExtents(cuboid);

        if (!cuboid.TryGetShapeExtentCode(longest, out ShapeExtentCode? longestCode)
            || !cuboid.TryGetShapeExtentCode(shortest, out ShapeExtentCode? shortestCode))
        {
            throw exception();
        }

        (longest, shortest) = getComparedShapeExtents(this);
        IExtent medium = GetComparedShapeExtent(null);

        return longestCode!.Value switch
        {
            ShapeExtentCode.Length => rotateToLength(),
            ShapeExtentCode.Width => rotateToWidth(),
            ShapeExtentCode.Height => rotateToHeight(),

            _ => throw exception(),
        };

        #region Local methods
        (IExtent, IExtent) getComparedShapeExtents(ICuboid cuboid)
        {
            IExtent longest = cuboid.GetComparedShapeExtent(ComparisonCode.Greater);
            IExtent shortest = cuboid.GetComparedShapeExtent(ComparisonCode.Less);

            return (longest, shortest);
        }

        ICuboid rotateToLength()
        {
            return shortestCode!.Value switch
            {
                ShapeExtentCode.Width => GetCuboid(longest, shortest, medium),
                ShapeExtentCode.Height => GetCuboid(longest, medium, shortest),

                _ => throw exception(),
            };
        }

        ICuboid rotateToWidth()
        {
            return shortestCode!.Value switch
            {
                ShapeExtentCode.Length => GetCuboid(shortest, longest, medium),
                ShapeExtentCode.Height => GetCuboid(medium, longest, shortest),

                _ => throw exception(),
            };
        }

        ICuboid rotateToHeight()
        {
            return shortestCode!.Value switch
            {
                ShapeExtentCode.Length => RotateSpatially(),
                ShapeExtentCode.Width => GetCuboid(medium, shortest, longest),

                _ => throw exception(),
            };
        }

        InvalidOperationException exception()
        {
            throw new InvalidOperationException(null);
        }
        #endregion
    }

    #region Override methods
    public override IRectangleFactory GetBaseFaceFactory()
    {
        return (IRectangleFactory)Factory.GetBaseFaceFactory();
    }

    public override IPlaneShape GetProjection(ShapeExtentCode perpendicular)
    {
        return Factory.CreateProjection(this, perpendicular)!;
    }

    public override ICylinderFactory GetTangentShapeFactory()
    {
        return Factory.TangentShapeFactory;
    }

    public override ICuboidFactory GetFactory()
    {
        return Factory;
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return GetRectangularShapeDiagonal(this, extentUnit);
    }
    #endregion
    #endregion
}
