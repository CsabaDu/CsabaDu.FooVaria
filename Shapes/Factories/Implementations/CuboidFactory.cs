namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public sealed class CuboidFactory : DryBodyFactory<ICuboid, IRectangle>, ICuboidFactory
{
    public CuboidFactory(IBulkBodyFactory spreadFactory, ICylinderFactory tangentShapeFactory, IRectangleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
    {
    }

    public override IRectangleFactory GetBaseFaceFactory()
    {
        return (IRectangleFactory)BaseFaceFactory;
    }

    public override ICuboid Create(IRectangle baseFace, IExtent height)
    {
        return new Cuboid(this, baseFace, height);
    }

    public ICuboid Create(IExtent length, IExtent width, IExtent height)
    {
        return new Cuboid(this, length, width, height);
    }

    public ICuboid CreateNew(ICuboid other)
    {
        return new Cuboid(other);
    }

    public IRectangle CreateBaseFace(IExtent length, IExtent width)
    {
        return GetBaseFaceFactory().Create(length, width);
    }

    public ICylinder CreateInnerTangentShape(ICuboid cuboid, ComparisonCode comparisonCode)
    {
        ICircle baseFace = cuboid.BaseFace.GetInnerTangentShape(comparisonCode);

        return CreateTangentShape(this, baseFace, cuboid);
    }

    public ICylinder CreateInnerTangentShape(ICuboid cuboid)
    {
        ICircle baseFace = cuboid.BaseFace.GetInnerTangentShape();

        return CreateTangentShape(this, baseFace, cuboid);
    }

    public ICylinder CreateOuterTangentShape(ICuboid cuboid)
    {
        ICircle baseFace = cuboid.BaseFace.GetInnerTangentShape();

        return CreateTangentShape(this, baseFace, cuboid);
    }

    //public IRectangle CreateProjection(ICuboid cuboid, ShapeExtentTypeCode perpendicular)
    //{
    //    IExtent horizontal = NullChecked(cuboid, nameof(cuboid)).GetShapeExtent(getPerpendicularShapeExtentTypeCode());
    //    IRectangleFactory factory = GetBaseFaceFactory();

    //    return perpendicular switch
    //    {
    //        ShapeExtentTypeCode.Length => CreateVerticalProjection(factory, horizontal, cuboid),
    //        ShapeExtentTypeCode.Width => CreateVerticalProjection(factory, horizontal, cuboid),
    //        ShapeExtentTypeCode.Height => factory.CreateNew(cuboid.BaseFace),

    //        _ => throw new InvalidOperationException(),
    //    };

    //    #region Local methods
    //    ShapeExtentTypeCode getPerpendicularShapeExtentTypeCode()
    //    {
    //        return perpendicular switch
    //        {
    //            ShapeExtentTypeCode.Length => ShapeExtentTypeCode.Width,
    //            ShapeExtentTypeCode.Width => ShapeExtentTypeCode.Length,
    //            ShapeExtentTypeCode.Height => ShapeExtentTypeCode.Height,

    //            _ => throw InvalidShapeExtentTypeCodeEnumArgumentException(perpendicular, nameof(perpendicular)),
    //        };
    //    }
    //    #endregion
    //}

    public ICylinder CreateTangentShape(ICuboid cuboid, SideCode sideCode)
    {
        return CreateTangentShape(this, cuboid, sideCode);
    }

    public IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode)
    {
        IExtent horizontal = NullChecked(cuboid, nameof(cuboid)).BaseFace.GetComparedShapeExtent(comparisonCode);
        IRectangleFactory factory = GetBaseFaceFactory();

        return CreateVerticalProjection(factory, horizontal, cuboid);
    }

    public override ICylinderFactory GetTangentShapeFactory()
    {
        return (ICylinderFactory)TangentShapeFactory;
    }

    public override ICuboid? CreateBaseShape(params IShapeComponent[] shapeComponents)
    {
        int count = GetShapeComponentsCount(shapeComponents);

        return count switch
        {
            1 => createCuboidFrom1Param(),
            2 => createCuboidFrom2Params(),
            3 => createCuboidFrom3Params(),

            _ => null,
        };

        #region Local methods
        ICuboid? createCuboidFrom1Param()
        {
            return shapeComponents[0] is ICuboid cuboid ?
                CreateNew(cuboid)
                : null;
        }

        ICuboid? createCuboidFrom2Params()
        {
            if (GetShapeExtent(shapeComponents[1]) is not IExtent height) return null;

            if (shapeComponents[0] is IRectangle rectangle) return Create(rectangle, height);

            return null;
        }

        ICuboid? createCuboidFrom3Params()
        {
            IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

            return shapeExtents != null ?
                Create(shapeExtents.First(), shapeExtents.ElementAt(1), shapeExtents.Last())
                : null;
        }
        #endregion
    }
}
