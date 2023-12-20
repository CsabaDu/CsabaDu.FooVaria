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
        public ISurface Create(ISurface other)
        {
            IArea area = (IArea)NullChecked(other, nameof(other)).GetSpreadMeasure();

            return GetSpreadFactory().Create(area);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IBulkSurfaceFactory GetSpreadFactory()
        {
            return (IBulkSurfaceFactory)SpreadFactory;
        }
        #endregion
        #endregion

        #region Abstract methods
        public IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            return perpendicular switch
            {
                ShapeExtentTypeCode.Radius => createCylinderVerticalProjection(),
                ShapeExtentTypeCode.Length => createCuboidVerticalProjection(ShapeExtentTypeCode.Width),
                ShapeExtentTypeCode.Width => createCuboidVerticalProjection(ShapeExtentTypeCode.Length),
                ShapeExtentTypeCode.Height => createHorizontalProjection(),

                _ => null,
            };

            #region Local methods
            IRectangle? createCylinderVerticalProjection()
            {
                if (dryBody is not ICylinder cylinder) return null;

                IExtent horizontal = cylinder.BaseFace.GetDiagonal();
                ICuboidFactory factory = (ICuboidFactory)cylinder.GetTangentShapeFactory();

                return createRectangle(factory, horizontal);
            }

            IRectangle? createCuboidVerticalProjection(ShapeExtentTypeCode shapeExtentTypeCode)
            {
                if (dryBody is not ICuboid cuboid) return null;

                IExtent horizontal = cuboid.GetShapeExtent(shapeExtentTypeCode);
                ICuboidFactory factory = (ICuboidFactory)cuboid.GetFactory();

                return createRectangle(factory, horizontal);
            }

            IRectangle? createRectangle(ICuboidFactory factory, IExtent horizontal)
            {
                IPlaneShapeFactory baseFaceFactory = factory.GetBaseFaceFactory();

                return (IRectangle)baseFaceFactory.Create(horizontal, dryBody.Height);
            }

            IPlaneShape createHorizontalProjection()
            {
                IEnumerable<IExtent> shapeExtents = dryBody.GetShapeExtents().SkipLast(1);
                IPlaneShapeFactory factory = dryBody.GetBaseFaceFactory();

                return (IPlaneShape)factory.Create(shapeExtents.ToArray());
            }
            #endregion
        }
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static TTangent CreateTangentShape<T, TTangent>(IShapeFactory<T, TTangent> factory,  T tangentShape, SideCode sideCode)
            where T : class, IShape, ITangentShape
            where TTangent : class, IShape, ITangentShape
        {
            return sideCode switch
            {
                SideCode.Outer => factory.CreateOuterTangentShape(tangentShape),
                SideCode.Inner => factory.CreateInnerTangentShape(tangentShape),

                _ => throw InvalidSidenCodeEnumArgumentException(sideCode),
            };
        }
        #endregion
        #endregion
    }
}
