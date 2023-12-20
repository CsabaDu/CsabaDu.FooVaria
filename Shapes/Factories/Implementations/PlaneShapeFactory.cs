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
        public abstract IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
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
