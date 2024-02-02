//namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

//public sealed class CylinderFactory : DryBodyFactory<ICylinder, ICircle>, ICylinderFactory
//{
//    #region Constructors
//    public CylinderFactory(IBulkBodyFactory spreadFactory, ICuboidFactory tangentShapeFactory, ICircleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
//    {
//    }
//    #endregion

//    #region Public methods
//    public ICylinder Create(IExtent radius, IExtent height)
//    {
//        return new Cylinder(this, radius, height);
//    }

//    public ICircle CreateBaseFace(IExtent radius)
//    {
//        return GetBaseFaceFactory().Create(radius);
//    }

//    public ICuboid CreateInnerTangentShape(ICylinder cylinder, IExtent tangentRectangleSide)
//    {
//        IRectangle baseFace = cylinder.BaseFace.GetInnerTangentShape(tangentRectangleSide);

//        return CreateTangentShape(this, baseFace, cylinder);
//    }

//    public ICuboid CreateInnerTangentShape(ICylinder cylinder)
//    {
//        IRectangle baseFace = cylinder.BaseFace.GetInnerTangentShape();

//        return CreateTangentShape(this, baseFace, cylinder);
//    }

//    public ICuboid CreateOuterTangentShape(ICylinder cylinder)
//    {
//        return CreateTangentShape(this, cylinder);
//    }

//    public ICuboid CreateTangentShape(ICylinder cylinder, SideCode sideCode)
//    {
//        return CreateTangentShape(this, cylinder, sideCode);
//    }

//    public IRectangle CreateVerticalProjection(ICylinder cylinder)
//    {
//        IExtent horizontal = NullChecked(cylinder, nameof(cylinder)).BaseFace.GetDiagonal();
//        IRectangleFactory factory = (IRectangleFactory)BaseFaceFactory.GetTangentShapeFactory();

//        return CreateVerticalProjection(factory, horizontal, cylinder)!;
//    }

//    public ICylinder CreateNew(ICylinder other)
//    {
//        return new Cylinder(other);
//    }

//    #region Override methods
//    public override ICylinder Create(ICircle baseFace, IExtent height)
//    {
//        return new Cylinder(this, baseFace, height);
//    }

//    public override IDryBody Create(IPlaneShape baseFace, IExtent height)
//    {
//        string paramName = nameof(baseFace);

//        if (NullChecked(baseFace, paramName) is ICircle circle) return Create(circle, height);

//        return GetTangentShapeFactory().Create(baseFace, height);
//    }

//    public override IDryBody? CreateBaseShape(params IShapeComponent[] shapeComponents)
//    {
//        return CreateDryBody(GetTangentShapeFactory(), this, shapeComponents);
//    }

//    public override ICircleFactory GetBaseFaceFactory()
//    {
//        return (ICircleFactory)BaseFaceFactory;
//    }

//    public override ICuboidFactory GetTangentShapeFactory()
//    {
//        return (ICuboidFactory)TangentShapeFactory;
//    }
//    #endregion
//    #endregion
//}
