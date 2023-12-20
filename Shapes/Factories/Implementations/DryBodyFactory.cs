
namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public abstract class DryBodyFactory : ShapeFactory, IDryBodyFactory
    {
        private protected DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory)
        {
            BaseFaceFactory = NullChecked(baseFaceFactory, nameof(baseFaceFactory));
        }

        public IPlaneShapeFactory BaseFaceFactory { get; init; }

        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);

        public IBody Create(IBody other)
        {
            IVolume volume = (IVolume)NullChecked(other, nameof(other)).GetSpreadMeasure();

            return GetSpreadFactory().Create(volume);
        }

        public abstract IPlaneShape CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
        public virtual IPlaneShapeFactory GetBaseFaceFactory()
        {
            return BaseFaceFactory;
        }

        public override sealed IBulkBodyFactory GetSpreadFactory()
        {
            return (IBulkBodyFactory)SpreadFactory;
        }
    }

    public abstract class DryBodyFactory<T, TBFace> : DryBodyFactory, IDryBodyFactory<T, TBFace>
        where T : class, IDryBody, ITangentShape
        where TBFace : class, IPlaneShape, ITangentShape
    {
        public DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }

        public override T Create(IPlaneShape baseFace, IExtent height)
        {
            string paramName = nameof(baseFace);

            if (NullChecked(baseFace, paramName) is TBFace validBaseFace) return Create(validBaseFace, height);

            throw ArgumentTypeOutOfRangeException(paramName, baseFace);
        }

        public abstract T Create(TBFace baseFace, IExtent height);
    }
}
