namespace CsabaDu.FooVaria.DryBodies.Types.Implementations;

internal sealed class Cuboid : DryBody<ICuboid, IRectangle>, ICuboid
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Cuboid"/> class by copying another cuboid.
    /// </summary>
    /// <param name="other">The other cuboid to copy.</param>
    internal Cuboid(ICuboid other) : base(other)
    {
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cuboid"/> class using a factory and shape extents.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="length">The length of the cuboid.</param>
    /// <param name="width">The width of the cuboid.</param>
    /// <param name="height">The height of the cuboid.</param>
    internal Cuboid(ICuboidFactory factory, IExtent length, IExtent width, IExtent height) : base(factory, length, width, height)
    {
        Factory = factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cuboid"/> class using a factory, base face, and height.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    /// <param name="baseFace">The base face of the cuboid.</param>
    /// <param name="height">The height of the cuboid.</param>
    internal Cuboid(ICuboidFactory factory, IRectangle baseFace, IExtent height) : base(factory, baseFace, height)
    {
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the factory used to create instances of the cuboid.
    /// </summary>
    public ICuboidFactory Factory { get; init; }

    #region Override properties
    /// <summary>
    /// Gets the extent of the cuboid for the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The extent of the cuboid.</returns>
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
    /// <summary>
    /// Gets the compared shape extent for the specified comparison code.
    /// </summary>
    /// <param name="comparisonCode">The comparison code.</param>
    /// <returns>The compared shape extent.</returns>
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

    /// <summary>
    /// Gets a new cuboid with the specified dimensions.
    /// </summary>
    /// <param name="length">The length of the cuboid.</param>
    /// <param name="width">The width of the cuboid.</param>
    /// <param name="height">The height of the cuboid.</param>
    /// <returns>A new cuboid.</returns>
    public ICuboid GetCuboid(IExtent length, IExtent width, IExtent height)
    {
        return Factory.Create(length, width, height);
    }

    /// <summary>
    /// Gets the inner tangent shape of the cuboid for the specified comparison code.
    /// </summary>
    /// <param name="comparisonCode">The comparison code.</param>
    /// <returns>The inner tangent shape of the cuboid.</returns>
    public ICylinder GetInnerTangentShape(ComparisonCode comparisonCode)
    {
        ICircle baseFace = BaseFace.GetInnerTangentShape(comparisonCode);

        return GetTangentShapeFactory().Create(baseFace, Height);
    }

    /// <summary>
    /// Gets the inner tangent shape of the cuboid.
    /// </summary>
    /// <returns>The inner tangent shape of the cuboid.</returns>
    public ICylinder GetInnerTangentShape()
    {
        return GetInnerTangentShape(ComparisonCode.Less);
    }

    /// <summary>
    /// Gets the length of the cuboid.
    /// </summary>
    /// <returns>The length of the cuboid.</returns>
    public IExtent GetLength()
    {
        return BaseFace.Length;
    }

    /// <summary>
    /// Gets the length of the cuboid for the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The length of the cuboid.</returns>
    public IExtent GetLength(ExtentUnit extentUnit)
    {
        return GetLength().GetMeasure(extentUnit);
    }

    /// <summary>
    /// Gets the outer tangent shape of the cuboid.
    /// </summary>
    /// <returns>The outer tangent shape of the cuboid.</returns>
    public ICylinder GetOuterTangentShape()
    {
        return Factory.CreateOuterTangentShape(this);
    }

    /// <summary>
    /// Gets a new instance of the cuboid by copying another cuboid.
    /// </summary>
    /// <param name="other">The other cuboid to copy.</param>
    /// <returns>A new instance of the cuboid.</returns>
    public override ICuboid GetNew(ICuboid other)
    {
        return Factory.CreateNew(other);
    }

    /// <summary>
    /// Gets the tangent shape of the cuboid for the specified side code.
    /// </summary>
    /// <param name="sideCode">The side code.</param>
    /// <returns>The tangent shape of the cuboid.</returns>
    public IShape GetTangentShape(SideCode sideCode)
    {
        return Factory.CreateTangentShape(this, sideCode);
    }

    /// <summary>
    /// Gets the vertical projection of the cuboid for the specified comparison code.
    /// </summary>
    /// <param name="comparisonCode">The comparison code.</param>
    /// <returns>The vertical projection of the cuboid.</returns>
    public IRectangle GetVerticalProjection(ComparisonCode comparisonCode)
    {
        return Factory.CreateVerticalProjection(this, comparisonCode);
    }

    /// <summary>
    /// Gets the width of the cuboid.
    /// </summary>
    /// <returns>The width of the cuboid.</returns>
    public IExtent GetWidth()
    {
        return BaseFace.Width;
    }

    /// <summary>
    /// Gets the width of the cuboid for the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The width of the cuboid.</returns>
    public IExtent GetWidth(ExtentUnit extentUnit)
    {
        return GetWidth().GetMeasure(extentUnit);
    }

    /// <summary>
    /// Rotates the cuboid horizontally.
    /// </summary>
    /// <returns>The rotated cuboid.</returns>
    public ICuboid RotateHorizontally()
    {
        IRectangle baseFace = BaseFace.RotateHorizontally();

        return GetDryBody(baseFace, Height);
    }

    /// <summary>
    /// Rotates the cuboid spatially.
    /// </summary>
    /// <returns>The rotated cuboid.</returns>
    public ICuboid RotateSpatially()
    {
        return (ICuboid)GetSimpleShape(GetSortedDimensions().ToArray());
    }

    /// <summary>
    /// Rotates the cuboid to match another dry body.
    /// </summary>
    /// <param name="other">The other dry body to match.</param>
    /// <returns>The rotated cuboid.</returns>
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
    /// <summary>
    /// Gets the base face factory of the cuboid.
    /// </summary>
    /// <returns>The base face factory.</returns>
    public override IRectangleFactory GetBaseFaceFactory()
    {
        return (IRectangleFactory)Factory.GetBaseFaceFactory();
    }

    /// <summary>
    /// Gets the projection of the cuboid for the specified shape extent code.
    /// </summary>
    /// <param name="perpendicular">The shape extent code.</param>
    /// <returns>The projection of the cuboid.</returns>
    public override IPlaneShape GetProjection(ShapeExtentCode perpendicular)
    {
        return Factory.CreateProjection(this, perpendicular)!;
    }

    /// <summary>
    /// Gets the tangent shape factory of the cuboid.
    /// </summary>
    /// <returns>The tangent shape factory.</returns>
    public override ICylinderFactory GetTangentShapeFactory()
    {
        return Factory.TangentShapeFactory;
    }

    /// <summary>
    /// Gets the factory used to create instances of the cuboid.
    /// </summary>
    /// <returns>The factory used to create instances of the cuboid.</returns>
    public override ICuboidFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets the diagonal of the cuboid for the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The diagonal of the cuboid.</returns>
    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return GetRectangularShapeDiagonal(this, extentUnit);
    }
    #endregion
    #endregion
}
