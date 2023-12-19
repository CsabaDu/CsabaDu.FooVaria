using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public sealed class CylinderFactory : DryBodyFactory<ICylinder, ICircle>, ICylinderFactory
    {
        public CylinderFactory(IBulkBodyFactory spreadFactory, ICuboidFactory tangentShapeFactory, ICircleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }

        public override ICylinder Create(ICircle baseFace, IExtent height)
        {
            throw new NotImplementedException();
        }

        public override ICylinder Create(IDryBody other)
        {
            throw new NotImplementedException();
        }

        public override ICylinder Create(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public ICylinder Create(IExtent radius, IExtent height)
        {
            throw new NotImplementedException();
        }

        public ICylinder Create(ICylinder other)
        {
            throw new NotImplementedException();
        }

        public ICircle CreateBaseFace(IExtent radius)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateInnerTangentShape(ICylinder circularShape, IExtent tangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateInnerTangentShape(ICylinder shape)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateOuterTangentShape(ICylinder shape)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateTangentShape(ICylinder shape, SideCode sideCode)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateVerticalProjection(ICylinder cylinder)
        {
            throw new NotImplementedException();
        }

        public override ICircleFactory GetBaseFaceFactory()
        {
            throw new NotImplementedException();
        }

        public override int GetShapeComponentCount()
        {
            return CylinderShapeExtentCount;
        }

        public override ICuboidFactory GetTangentShapeFactory()
        {
            throw new NotImplementedException();
        }
    }
}
