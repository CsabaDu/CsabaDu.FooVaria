namespace CsabaDu.FooVaria.DryBodies.Factories.Implementations;

public sealed class CuboidFactory(IBulkBodyFactory bulkSpreadFactory, IRectangleFactory baseFaceFactory, ICylinderFactory tangentShapeFactory)
    : DryBodyFactory<ICuboid, IRectangle>(bulkSpreadFactory), ICuboidFactory
{
    #region Properties
    public ICylinderFactory TangentShapeFactory { get; init; } = NullChecked(tangentShapeFactory, nameof(tangentShapeFactory));
    public IRectangleFactory BaseFaceFactory { get; init; } = NullChecked(baseFaceFactory, nameof(baseFaceFactory));

    #endregion

    #region Public methods
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
        return BaseFaceFactory.Create(length, width);
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

    public ICylinder CreateTangentShape(ICuboid cuboid, SideCode sideCode)
    {
        return CreateTangentShape(this, cuboid, sideCode);
    }

    public IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode)
    {
        IExtent horizontal = NullChecked(cuboid, nameof(cuboid)).BaseFace.GetComparedShapeExtent(comparisonCode);

        return CreateVerticalProjection(BaseFaceFactory, horizontal, cuboid);
    }

    #region Override methods
    public override ICuboid Create(IRectangle baseFace, IExtent height)
    {
        return new Cuboid(this, baseFace, height);
    }

    public override IDryBody Create(IPlaneShape baseFace, IExtent height)
    {
        const string paramName = nameof(baseFace);

        if (NullChecked(baseFace, paramName) is IRectangle rectangle) return Create(rectangle, height);

        return TangentShapeFactory.Create(baseFace, height);
    }

    public override IDryBody? CreateShape(params IShapeComponent[] shapeComponents)
    {
        return CreateDryBody(this, TangentShapeFactory, shapeComponents);
    }

    public override IRectangleFactory GetBaseFaceFactory()
    {
        return BaseFaceFactory;
    }

    public override ICylinderFactory GetTangentShapeFactory()
    {
        return TangentShapeFactory;
    }
    #endregion
    #endregion
}
