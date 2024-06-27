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
        protected static IPlaneShape? CreatePlaneShape(ICircleFactory circleFactory, IShapeComponent shapeComponent)
        {
            return shapeComponent switch
            {
                Circle circle => circle.GetNew(),
                Rectangle rectangle => rectangle.GetNew(),
                IExtent radius => circleFactory.Create(radius),

                _ => null,
            };
        }

        protected static IPlaneShape? CreatePlaneShape(IRectangleFactory rectangleFactory, params IShapeComponent[] shapeComponents)
        {
            IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

            return shapeExtents is not null ?
                rectangleFactory.Create(shapeExtents.First(), shapeExtents.Last())
                : null;
        }
        #endregion
        #endregion
    }
}
