namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public sealed class RectangleFactory : PlaneShapeFactory, IRectangleFactory
    {
        #region Constructors
        public RectangleFactory(IBulkSurfaceFactory spreadFactory, ICircleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }
        #endregion

        #region Public methods
        public override IRectangle Create(params IQuantifiable[] shapeComponents)
        {
            int count = GetValidShapeComponentsCount(shapeComponents);

            return count switch
            {
                1 => createRectangleFromOneParam(),
                2 => createRectangleFromTwoParams(),

                _ => throw CountArgumentOutOfRangeException(count, nameof(shapeComponents)),

            };

            #region Local methods
            IRectangle createRectangleFromOneParam()
            {
                if (shapeComponents[0] is IRectangle rectangle) return Create(rectangle);

                throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), shapeComponents);
            }

            IRectangle createRectangleFromTwoParams()
            {
                IEnumerable<IExtent> shapeExtents = GetShapeExtents(shapeComponents);

                return Create(shapeExtents.First(), shapeExtents.Last());
            }
            #endregion
        }

        public IRectangle Create(IExtent length, IExtent width)
        {
            return new Rectangle(this, length, width);
        }

        public IRectangle Create(IRectangle other)
        {
            return new Rectangle(other);
        }

        public ICircle CreateInnerTangentShape(IRectangle rectangle, ComparisonCode comparisonCode)
        {
            IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetComparedShapeExtent(comparisonCode);

            return CreateCircle(diagonal);
        }

        public ICircle CreateInnerTangentShape(IRectangle rectangle)
        {
            return CreateInnerTangentShape(rectangle, ComparisonCode.Less);
        }

        public ICircle CreateOuterTangentShape(IRectangle rectangle)
        {
            IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetDiagonal();

            return CreateCircle(diagonal);
        }

        public ICircle CreateTangentShape(IRectangle rectangle, SideCode sideCode)
        {
            return CreateTangentShape(this, rectangle, sideCode);
        }

        #region Override methods
        public override ICircleFactory GetTangentShapeFactory()
        {
            return (ICircleFactory)TangentShapeFactory;
        }
        #endregion
        #endregion

        #region Private methods
        private ICircle CreateCircle(IExtent diagonal)
        {
            IExtent radius = (IExtent)diagonal.Divide(2);

            return GetTangentShapeFactory().Create(radius);
        }
        #endregion
    }
}
