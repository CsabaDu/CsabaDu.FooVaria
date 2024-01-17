namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public abstract class PlaneShapeFactory : ShapeFactory, IPlaneShapeFactory
    {
        #region Constructors
        private protected PlaneShapeFactory(IBulkSurfaceFactory spreadFactory, ITangentShapeFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }
        #endregion

        #region Public methods
        public IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentCode perpendicular)
        {
            if (dryBody?.IsValidShapeExtentCode(perpendicular) != true) return null;

            return perpendicular switch
            {
                ShapeExtentCode.Radius => createCylinderVerticalProjection(),
                ShapeExtentCode.Length => createCuboidVerticalProjection(),
                ShapeExtentCode.Width => createCuboidVerticalProjection(),
                ShapeExtentCode.Height => createHorizontalProjection(),

                _ => null,
            };

            #region Local methods
            IRectangle createCylinderVerticalProjection()
            {
                IExtent horizontal = dryBody.GetBaseFace().GetDiagonal();
                ICuboidFactory factory = (ICuboidFactory)dryBody.GetTangentShapeFactory();

                return createRectangle(factory, horizontal);
            }

            IRectangle createCuboidVerticalProjection()
            {
                perpendicular = perpendicular == ShapeExtentCode.Length ?
                    ShapeExtentCode.Width
                    : ShapeExtentCode.Length;

                IExtent horizontal = dryBody.GetShapeExtent(perpendicular);
                ICuboidFactory factory = (ICuboidFactory)dryBody.GetFactory();

                return createRectangle(factory, horizontal);
            }

            IRectangle createRectangle(ICuboidFactory factory, IExtent horizontal)
            {
                IRectangleFactory baseFaceFactory = (IRectangleFactory)factory.GetBaseFaceFactory();

                return baseFaceFactory.Create(horizontal, dryBody.Height);
            }

            IPlaneShape createHorizontalProjection()
            {
                IEnumerable<IExtent> shapeExtents = dryBody.GetShapeExtents().SkipLast(1);
                IPlaneShapeFactory factory = dryBody.GetBaseFaceFactory();

                return (IPlaneShape)factory.CreateBaseShape(shapeExtents.ToArray())!;
            }
            #endregion
        }

        #region Override methods
        #region Sealed methods
        public override sealed IBulkSurfaceFactory GetSpreadFactory()
        {
            return (IBulkSurfaceFactory)SpreadFactory;
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static IPlaneShape? CreatePlaneShape(IRectangleFactory rectangleFactory, params IShapeComponent[] shapeComponents)
        {
            int count = GetShapeComponentsCount(shapeComponents);

            if (count == 0) return null;

            IShapeComponent firstItem = shapeComponents[0];

            return count switch
            {
                1 => createPlaneSapeFrom1Param(),
                2 => createPlaneSapeFrom2Params(),

                _ => null,

            };

            #region Local methods
            IPlaneShape? createPlaneSapeFrom1Param()
            {
                if (firstItem is IRectangle rectangle) return rectangleFactory.CreateNew(rectangle);

                if (firstItem is ICircle circle) return circle.GetNew(circle);

                return null;
            }

            IPlaneShape? createPlaneSapeFrom2Params()
            {
                IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

                return shapeExtents != null ?
                    rectangleFactory.Create(shapeExtents.First(), shapeExtents.Last())
                    : null;
            }
            #endregion
        }
        #endregion
        #endregion
    }
}
