namespace CsabaDu.FooVaria.DryBodies.Factories.Implementations;

public sealed class CylinderFactory(IBulkBodyFactory bulkSpreadFactory, ICircleFactory baseFaceFactory, ICuboidFactory tangentShapeFactory)
    : DryBodyFactory<ICylinder, ICircle>(bulkSpreadFactory), ICylinderFactory
{
    #region Properties
    public ICuboidFactory TangentShapeFactory { get; init; } = NullChecked(tangentShapeFactory, nameof(tangentShapeFactory));
    public ICircleFactory BaseFaceFactory { get; init; } = NullChecked(baseFaceFactory, nameof(baseFaceFactory));

    #endregion

    #region Public methods
    public ICylinder Create(IExtent radius, IExtent height)
    {
        return new Cylinder(this, radius, height);
    }

    public ICircle CreateBaseFace(IExtent radius)
    {
        return BaseFaceFactory.Create(radius);
    }

    public ICuboid CreateInnerTangentShape(ICylinder cylinder, IExtent tangentRectangleSide)
    {
        IRectangle baseFace = cylinder.BaseFace.GetInnerTangentShape(tangentRectangleSide);

        return CreateTangentShape(this, baseFace, cylinder);
    }

    public ICuboid CreateInnerTangentShape(ICylinder cylinder)
    {
        IRectangle baseFace = cylinder.BaseFace.GetInnerTangentShape();

        return CreateTangentShape(this, baseFace, cylinder);
    }

    public ICuboid CreateOuterTangentShape(ICylinder cylinder)
    {
        return CreateTangentShape(this, cylinder);
    }

    public ICuboid CreateTangentShape(ICylinder cylinder, SideCode sideCode)
    {
        return CreateTangentShape(this, cylinder, sideCode);
    }

    public IRectangle CreateVerticalProjection(ICylinder cylinder)
    {
        IExtent horizontal = NullChecked(cylinder, nameof(cylinder)).BaseFace.GetDiagonal();
        IRectangleFactory factory = (IRectangleFactory)BaseFaceFactory.TangentShapeFactory;

        return CreateVerticalProjection(factory, horizontal, cylinder)!;
    }

    public ICylinder CreateNew(ICylinder other)
    {
        return new Cylinder(other);
    }

    #region Override methods
    public override ICylinder Create(ICircle baseFace, IExtent height)
    {
        return new Cylinder(this, baseFace, height);
    }

    public override IDryBody Create(IPlaneShape baseFace, IExtent height)
    {
        const string paramName = nameof(baseFace);

        if (NullChecked(baseFace, paramName) is ICircle circle) return Create(circle, height);

        return TangentShapeFactory.Create(baseFace, height);
    }

    public override IDryBody? CreateShape(params IShapeComponent[] shapeComponents)
    {
        int count = shapeComponents?.Length ?? 0;

        return count switch
        {
            1 => CreateDryBody(shapeComponents![0]),
            2 => CreateDryBody(TangentShapeFactory, this, shapeComponents!),
            3 => CreateDryBody(TangentShapeFactory, shapeComponents!),

            _ => null,
        };
    }

    public override ICircleFactory GetBaseFaceFactory()
    {
        return BaseFaceFactory;
    }

    public override ICuboidFactory GetTangentShapeFactory()
    {
        return TangentShapeFactory;
    }
    #endregion
    #endregion
}
