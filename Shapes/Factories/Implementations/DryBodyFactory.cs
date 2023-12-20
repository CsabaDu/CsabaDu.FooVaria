namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public abstract class DryBodyFactory : ShapeFactory, IDryBodyFactory
    {
        #region Constructors
        private protected DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory)
        {
            BaseFaceFactory = NullChecked(baseFaceFactory, nameof(baseFaceFactory));
        }
        #endregion

        #region Properties
        public IPlaneShapeFactory BaseFaceFactory { get; init; }
        #endregion

        #region Public methods
        public IBody Create(IBody other)
        {
            IVolume volume = (IVolume)NullChecked(other, nameof(other)).GetSpreadMeasure();

            return GetSpreadFactory().Create(volume);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IBulkBodyFactory GetSpreadFactory()
        {
            return (IBulkBodyFactory)SpreadFactory;
        }
        #endregion
        #endregion

        #region Virtual methods
        public virtual IPlaneShapeFactory GetBaseFaceFactory()
        {
            return BaseFaceFactory;
        }
        #endregion

        #region Abstract methods
        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);
        public abstract IPlaneShape CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
        #endregion
        #endregion
    }

    public abstract class DryBodyFactory<T, TBFace> : DryBodyFactory, IDryBodyFactory<T, TBFace>
        where T : class, IDryBody, ITangentShape
        where TBFace : class, IPlaneShape, ITangentShape
    {
        #region Constructors
        public DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed T Create(IPlaneShape baseFace, IExtent height)
        {
            string paramName = nameof(baseFace);

            if (NullChecked(baseFace, paramName) is TBFace validBaseFace) return Create(validBaseFace, height);

            throw ArgumentTypeOutOfRangeException(paramName, baseFace);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T Create(TBFace baseFace, IExtent height);
        #endregion
        #endregion
    }
}
