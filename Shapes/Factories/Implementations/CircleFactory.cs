using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;

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

        public override ICircle Create(IPlaneShape other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Circle circle =>  Create(circle),
                Rectangle rectangle => rectangle.GetOuterTangentShape(),

                _ => throw new InvalidOperationException(null),
            };
        }

        public ICircle Create(IExtent radius)
        {
            return new Circle(this, radius);
        }

        public ICircle Create(ICircle other)
        {
            return new Circle(other);
        }

        public override ICircle Create(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
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

        public override int GetShapeComponentCount()
        {
            return CircleShapeExtentCount;
        }

        public override IRectangleFactory GetTangentShapeFactory()
        {
            return (IRectangleFactory)TangentShapeFactory;
        }
    }
}
