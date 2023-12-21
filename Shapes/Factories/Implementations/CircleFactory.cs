namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public sealed class CircleFactory : PlaneShapeFactory, ICircleFactory
    {
        public CircleFactory(IBulkSurfaceFactory spreadFactory, IRectangleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }

        public ICircle Create(IExtent radius)
        {
            return new Circle(this, radius);
        }

        public ICircle CreateNew(ICircle other)
        {
            return new Circle(other);
        }

        public override ICircle CreateBaseShape(params IQuantifiable[] shapeComponents)
        {
            int count = GetValidShapeComponentsCount(shapeComponents);

            switch (count)
            {
                case 1:
                    if (shapeComponents[0] is ICircle circle) return CreateNew(circle);
                    if (shapeComponents[0] is IExtent radius) return Create(radius);
                    throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), shapeComponents);

                default:
                    throw CountArgumentOutOfRangeException(count, nameof(shapeComponents));
            }
        }

        public IRectangle CreateInnerTangentShape(ICircle circle, IExtent tangentRectangleSide)
        {
            IExtent otherSide = ShapeExtents.GetInnerTangentRectangleSide(circle, tangentRectangleSide);

            return GetTangentShapeFactory().Create(tangentRectangleSide, otherSide);
        }

        public IRectangle CreateInnerTangentShape(ICircle circle)
        {
            IExtent side = ShapeExtents.GetInnerTangentRectangleSide(circle);

            return GetTangentShapeFactory().Create(side, side);
        }

        public IRectangle CreateOuterTangentShape(ICircle circle)
        {
            return (IRectangle)GetTangentShapeFactory().CreateNew(circle);
        }

        public IRectangle CreateTangentShape(ICircle circle, SideCode sideCode)
        {
            return CreateTangentShape(this, circle, sideCode);
        }

        public override IRectangleFactory GetTangentShapeFactory()
        {
            return (IRectangleFactory)TangentShapeFactory;
        }
    }
}
