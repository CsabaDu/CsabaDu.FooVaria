using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;


namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal sealed class Rectangle : PlaneShape, IRectangle
    {
        internal Rectangle(IRectangle other) : base(other)
        {
            Length = other.Length;
            Width = other.Width;
        }

        internal Rectangle(IRectangleFactory factory, IExtent length, IExtent width) : base(factory, length, width)
        {
            Length = length;
            Width = width;
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
        {
            ShapeExtentTypeCode.Length => Length,
            ShapeExtentTypeCode.Width => Width,

            _ => null,
        };

        public IExtent Length { get; init; }
        public IExtent Width { get; init; }

        public IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode)
        {
            _ = NullChecked(comparisonCode, nameof(comparisonCode));

            IEnumerable<IExtent> shapeExtents = GetSortedDimensions();

            return comparisonCode switch
            {
                ComparisonCode.Greater => shapeExtents.Last(),
                ComparisonCode.Less => shapeExtents.First(),

                _ => throw InvalidComparisonCodeEnumArgumentException(comparisonCode!.Value),
            };
        }

        public override IEnumerable<IExtent> GetDimensions()
        {
            return GetShapeExtents();
        }

        public ICircle GetInnerTangentShape(ComparisonCode comparisonCode)
        {
            IExtent radius = (IExtent)GetComparedShapeExtent(comparisonCode).Divide(2);

            return GetTangentShapeFactory().Create(radius);
        }

        public ICircle GetInnerTangentShape()
        {
            return GetInnerTangentShape(ComparisonCode.Less);
        }

        public IExtent GetLength()
        {
            return Length;
        }

        public IExtent GetLength(ExtentUnit extentUnit)
        {
            return Length.GetMeasure(extentUnit);
        }

        public ICircle GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public override int GetShapeExtentCount()
        {
            return RectangleShapeExtentCount;
        }

        //public override IRectangle GetShape(params IExtent[] shapeExtents)
        //{
        //    throw new NotImplementedException();
        //}

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetFactory().CreateTangentShape(this, sideCode);
        }

        public override IRectangleFactory GetFactory()
        {
            return (IRectangleFactory)Factory;
        }

        public override ICircleFactory GetTangentShapeFactory()
        {
            return (ICircleFactory)GetFactory().TangentShapeFactory;
        }

        public IExtent GetWidth()
        {
            return Width;
        }

        public IExtent GetWidth(ExtentUnit extentUnit)
        {
            return Width.GetMeasure(extentUnit);
        }

        public IRectangle RotateHorizontally()
        {
            return (IRectangle)GetShape(GetSortedDimensions());
        }
    }
}
