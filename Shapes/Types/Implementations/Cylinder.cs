using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;


namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal sealed class Cylinder : DryBody<ICylinder, ICircle>, ICylinder
    {
        internal Cylinder(ICylinder other) : base(other)
        {
        }

        internal Cylinder(ICylinderFactory factory, IExtent radius, IExtent height) : base(factory, radius, height)
        {
        }

        internal Cylinder(ICylinderFactory factory, ICircle baseFace, IExtent height) : base(factory, baseFace, height)
        {
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => throw new NotImplementedException();

        public ICuboid GetInnerTangentShape(IExtent innerTangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public ICuboid GetInnerTangentShape()
        {
            throw new NotImplementedException();
        }

        public ICuboid GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public override ICircleFactory GetBaseFaceFactory()
        {
            return (ICircleFactory)base.GetBaseFaceFactory();
        }

        public override IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public IExtent GetRadius()
        {
            return BaseFace.Radius;
        }

        public IExtent GetRadius(ExtentUnit extentUnit)
        {
            return GetRadius().GetMeasure(extentUnit);
        }

        public override int GetShapeExtentCount()
        {
            return CylinderShapeExtentCount;
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetFactory().CreateTangentShape(this, sideCode);
        }

        public override ICuboidFactory GetTangentShapeFactory()
        {
            return (ICuboidFactory)GetFactory().TangentShapeFactory;
        }
        public IRectangle GetVerticalProjection()
        {
            return GetFactory().CreateVerticalProjection(this);
        }

        public override ICylinderFactory GetFactory()
        {
            return (ICylinderFactory)Factory;
        }
    }
}
