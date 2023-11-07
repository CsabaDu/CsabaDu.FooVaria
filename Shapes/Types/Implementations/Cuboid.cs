using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;


namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal sealed class Cuboid : DryBody<ICuboid, IRectangle>, ICuboid
    {
        internal Cuboid(ICuboid other) : base(other)
        {
        }

        internal Cuboid(ICuboidFactory factory, IExtent length, IExtent width, IExtent height) : base(factory, length, width, height)
        {
        }

        internal Cuboid(ICuboidFactory factory, IRectangle baseFace, IExtent height) : base(factory, baseFace, height)
        {
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
        {
            ShapeExtentTypeCode.Length => BaseFace.Length,
            ShapeExtentTypeCode.Width => BaseFace.Width,
            ShapeExtentTypeCode.Height => Height,

            _ => null,
        };

        public override IRectangleFactory GetBaseFaceFactory()
        {
            return (IRectangleFactory)base.GetBaseFaceFactory();
        }

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

        public ICylinder GetInnerTangentShape(ComparisonCode comparisonCode)
        {
            ICircle baseFace = BaseFace.GetInnerTangentShape(comparisonCode);

            return GetTangentShapeFactory().Create(baseFace, Height);
        }

        public ICylinder GetInnerTangentShape()
        {
            return GetInnerTangentShape(ComparisonCode.Less);
        }

        public IExtent GetLength()
        {
            throw new NotImplementedException();
        }

        public IExtent GetLength(ExtentUnit extentUnit)
        {
            throw new NotImplementedException();
        }

        public ICylinder GetOuterTangentShape()
        {
            return GetFactory().CreateOuterTangentShape(this);
        }

        public override IRectangle GetProjection(ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public override int GetShapeExtentCount()
        {
            return CuboidShapeExtentCount;
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetFactory().CreateTangentShape(this, sideCode);
        }

        public override ICylinderFactory GetTangentShapeFactory()
        {
            return (ICylinderFactory)GetFactory().TangentShapeFactory;
        }
        public IRectangle GetVerticalProjection(ComparisonCode comparisonCode)
        {
            return GetFactory().CreateVerticalProjection(this, comparisonCode);
        }

        public IExtent GetWidth()
        {
            return BaseFace.Width;
        }

        public IExtent GetWidth(ExtentUnit extentUnit)
        {
            return GetWidth().GetMeasure(extentUnit);
        }

        public ICuboid RotateHorizontally()
        {
            IRectangle baseFace = BaseFace.RotateHorizontally();

            return GetDryBody(baseFace, Height);
        }

        public ICuboid RotateSpatially()
        {
            return (ICuboid)GetShape(GetSortedDimensions());
        }

        public override ICuboidFactory GetFactory()
        {
            return (ICuboidFactory)Factory;
        }
    }
}
