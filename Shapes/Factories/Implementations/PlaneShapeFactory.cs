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
        public ISurface CreateNew(ISurface other)
        {
            IArea area = (IArea)NullChecked(other, nameof(other)).GetSpreadMeasure();

            return GetSpreadFactory().Create(area);
        }

        public IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            if (dryBody?.IsValidShapeExtentTypeCode(perpendicular) != true) return null;

            return perpendicular switch
            {
                ShapeExtentTypeCode.Radius => createCylinderVerticalProjection(),
                ShapeExtentTypeCode.Length => createCuboidVerticalProjection(),
                ShapeExtentTypeCode.Width => createCuboidVerticalProjection(),
                ShapeExtentTypeCode.Height => createHorizontalProjection(),

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
                perpendicular = perpendicular == ShapeExtentTypeCode.Length ?
                    ShapeExtentTypeCode.Width
                    : ShapeExtentTypeCode.Length;

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
        #endregion
        #endregion
    }
}
