namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public sealed class CylinderFactory : DryBodyFactory<ICylinder, ICircle>, ICylinderFactory
{
    public CylinderFactory(IBulkBodyFactory spreadFactory, ICuboidFactory tangentShapeFactory, ICircleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
    {
    }

    public override ICylinder Create(ICircle baseFace, IExtent height)
    {
        return new Cylinder(this, baseFace, height);
    }

    public ICylinder Create(IExtent radius, IExtent height)
    {
        return new Cylinder(this, radius, height);
    }

    public ICylinder CreateNew(ICylinder other)
    {
        return new Cylinder(other);
    }

    public override ICylinder? CreateBaseShape(params IShapeComponent[] shapeComponents)
    {
        int count = GetShapeComponentsCount(shapeComponents);

        return count switch
        {
            1 => createCylinderFrom1Param(),
            2 => createCylinderFrom2Params(),

            _ => null,

        };

        #region Local methods
        ICylinder? createCylinderFrom1Param()
        {
            return shapeComponents[0] is ICylinder cylinder ?
                CreateNew(cylinder)
                : null;
        }

        ICylinder? createCylinderFrom2Params()
        {
            if (GetShapeExtent(shapeComponents[1]) is not IExtent height) return null;

            IShapeComponent first = shapeComponents[0];

            if (first is ICircle circle) return Create(circle, height);

            if (GetShapeExtent(first) is IExtent radius) return Create(radius, height);

            return null;
        }
        #endregion
    }

    public ICircle CreateBaseFace(IExtent radius)
    {
        return GetBaseFaceFactory().Create(radius);
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
        IRectangleFactory factory = (IRectangleFactory)BaseFaceFactory.GetTangentShapeFactory();

        return CreateVerticalProjection(factory, horizontal, cylinder)!;
    }

    public override ICircleFactory GetBaseFaceFactory()
    {
        return (ICircleFactory)BaseFaceFactory;
    }

    public override ICuboidFactory GetTangentShapeFactory()
    {
        return (ICuboidFactory)TangentShapeFactory;
    }
}
