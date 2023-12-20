using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
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

        public ICuboid Create(ICuboid other)
        {
            return new Cuboid(other);
        }

        public override ICuboid Create(IDryBody other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Cylinder cylinder => cylinder.GetOuterTangentShape(),
                Cuboid cuboid => Create(cuboid),

                _ => throw ArgumentTypeOutOfRangeException(nameof(other), other),
            };
        }

        public IRectangle CreateBaseFace(IExtent length, IExtent width)
        {
            return GetBaseFaceFactory().Create(length, width);
        }

        public ICylinder CreateInnerTangentShape(ICuboid rectangularShape, ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateInnerTangentShape(ICuboid cuboid)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateOuterTangentShape(ICuboid cuboid)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateProjection(ICuboid cuboid, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateTangentShape(ICuboid cuboid, SideCode sideCode)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public override ICylinderFactory GetTangentShapeFactory()
        {
            return (ICylinderFactory)TangentShapeFactory;
        }

        public override IBaseShape Create(params IQuantifiable[] rateComponents)
        {
            throw new NotImplementedException();
        }
    }
}
