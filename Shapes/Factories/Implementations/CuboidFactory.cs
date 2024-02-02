//namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

//public sealed class CuboidFactory : DryBodyFactory<ICuboid, IRectangle>, ICuboidFactory
//{
//    #region Constructors
//    public CuboidFactory(IBulkBodyFactory spreadFactory, ICylinderFactory tangentShapeFactory, IRectangleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
//    {
//    }
//    #endregion

//    #region Public methods
//    public ICuboid Create(IExtent length, IExtent width, IExtent height)
//    {
//        return new Cuboid(this, length, width, height);
//    }

//    public ICuboid CreateNew(ICuboid other)
//    {
//        return new Cuboid(other);
//    }

//    public IRectangle CreateBaseFace(IExtent length, IExtent width)
//    {
//        return GetBaseFaceFactory().Create(length, width);
//    }

//    public ICylinder CreateInnerTangentShape(ICuboid cuboid, ComparisonCode comparisonCode)
//    {
//        ICircle baseFace = cuboid.BaseFace.GetInnerTangentShape(comparisonCode);

//        return CreateTangentShape(this, baseFace, cuboid);
//    }

//    public ICylinder CreateInnerTangentShape(ICuboid cuboid)
//    {
//        ICircle baseFace = cuboid.BaseFace.GetInnerTangentShape();

//        return CreateTangentShape(this, baseFace, cuboid);
//    }

//    public ICylinder CreateOuterTangentShape(ICuboid cuboid)
//    {
//        ICircle baseFace = cuboid.BaseFace.GetInnerTangentShape();

//        return CreateTangentShape(this, baseFace, cuboid);
//    }

//    public ICylinder CreateTangentShape(ICuboid cuboid, SideCode sideCode)
//    {
//        return CreateTangentShape(this, cuboid, sideCode);
//    }

//    public IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode)
//    {
//        IExtent horizontal = NullChecked(cuboid, nameof(cuboid)).BaseFace.GetComparedShapeExtent(comparisonCode);
//        IRectangleFactory factory = GetBaseFaceFactory();

//        return CreateVerticalProjection(factory, horizontal, cuboid);
//    }

//    #region Override methods
//    public override ICuboid Create(IRectangle baseFace, IExtent height)
//    {
//        return new Cuboid(this, baseFace, height);
//    }

//    public override IDryBody Create(IPlaneShape baseFace, IExtent height)
//    {
//        string paramName = nameof(baseFace);

//        if (NullChecked(baseFace, paramName) is IRectangle rectangle) return Create(rectangle, height);

//        return GetTangentShapeFactory().Create(baseFace, height);
//    }

//    public override IDryBody? CreateBaseShape(params IShapeComponent[] shapeComponents)
//    {
//        return CreateDryBody(this, GetTangentShapeFactory(), shapeComponents);
//    }

//    public override IRectangleFactory GetBaseFaceFactory()
//    {
//        return (IRectangleFactory)BaseFaceFactory;
//    }

//    public override ICylinderFactory GetTangentShapeFactory()
//    {
//        return (ICylinderFactory)TangentShapeFactory;
//    }
//    #endregion
//    #endregion
//}
