using static CsabaDu.FooVaria.Shapes.Statics.ShapeExtents;
using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;


namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal sealed class Circle : PlaneShape, ICircle
    {
        internal Circle(ICircle other) : base(other)
        {
            Radius = other.Radius;
        }

        internal Circle(ICircleFactory factory, IExtent radius) : base(factory, radius)
        {
            Radius = radius;
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => throw new NotImplementedException();

        public IExtent Radius { get; init; }

        public override IEnumerable<IExtent> GetDimensions()
        {
            for (int i = 0; i < GetShapeExtentCount(); i++)
            {
                yield return GetDiagonal();
            }
        }

        public IRectangle GetInnerTangentShape(IExtent innerTangentRectangleSide)
        {
            IExtent otherSide = GetInnerTangentRectangleSide(this, innerTangentRectangleSide);

            return GetTangentShapeFactory().Create(innerTangentRectangleSide, otherSide);
        }

        public IRectangle GetInnerTangentShape()
        {
            IExtent diagonal = GetDiagonal();
            decimal diagonalQuantity = diagonal.DefaultQuantity;
            diagonalQuantity *= diagonalQuantity;
            diagonalQuantity /= 2;
            double legQuantity = Math.Sqrt(decimal.ToDouble(diagonalQuantity));
            IExtent leg = diagonal.GetMeasure(legQuantity, default(ExtentUnit)); 

            return GetInnerTangentShape(leg);
        }

        public IRectangle GetOuterTangentShape()
        {
            IExtent diagonal = GetDiagonal();

            return GetTangentShapeFactory().Create(diagonal, diagonal);
        }

        public override int GetShapeExtentCount()
        {
            return CircleShapeExtentCount;
        }

        public override ICircleFactory GetFactory()
        {
            return (ICircleFactory)Factory;
        }

        public IExtent GetRadius()
        {
            return Radius;
        }

        public IExtent GetRadius(ExtentUnit extentUnit)
        {
            return Radius.GetMeasure(extentUnit);
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetFactory().CreateTangentShape(this, sideCode);
        }

        public override IRectangleFactory GetTangentShapeFactory()
        {
            return (IRectangleFactory)GetFactory().TangentShapeFactory;
        }
    }
}
