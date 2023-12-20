namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public sealed class CircleFactory : PlaneShapeFactory, ICircleFactory
    {
        public CircleFactory(IBulkSurfaceFactory spreadFactory, IRectangleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }

        public override IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public ICircle Create(IExtent radius)
        {
            return new Circle(this, radius);
        }

        public ICircle Create(ICircle other)
        {
            return new Circle(other);
        }

        public override ICircle Create(params IQuantifiable[] shapeComponents)
        {
            int count = GetValidShapeComponentsCount(shapeComponents);

            switch (count)
            {
                case 1:
                    if (shapeComponents[0] is ICircle circle) return Create(circle);
                    if (shapeComponents[0] is IExtent radius) return Create(radius);
                    throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), shapeComponents);

                default:
                    throw CountArgumentOutOfRangeException(count, nameof(shapeComponents));
            }
        }

        public IRectangle CreateInnerTangentShape(ICircle circle, IExtent tangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateInnerTangentShape(ICircle circle)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateOuterTangentShape(ICircle circle)
        {
            return (IRectangle)GetTangentShapeFactory().Create(circle);
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
