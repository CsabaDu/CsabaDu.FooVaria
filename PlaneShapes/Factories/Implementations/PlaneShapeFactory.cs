namespace CsabaDu.FooVaria.PlaneShapes.Factories.Implementations
{
    public abstract class PlaneShapeFactory(IBulkSurfaceFactory bulkSpreadFactory) : SimpleShapeFactory, IPlaneShapeFactory
    {
        #region Properties
        public IBulkSurfaceFactory BulkSurfaceFactory { get; init; } = NullChecked(bulkSpreadFactory, nameof(bulkSpreadFactory));
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed IBulkSurfaceFactory GetBulkSpreadFactory()
        {
            return BulkSurfaceFactory;
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
