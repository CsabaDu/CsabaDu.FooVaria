namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public abstract class DryBodyFactory : ShapeFactory, IDryBodyFactory
    {
        private protected DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory)
        {
            BaseFaceFactory = NullChecked(baseFaceFactory, nameof(baseFaceFactory));
        }

        public IPlaneShapeFactory BaseFaceFactory { get; init; }

        public override sealed IBulkBody Create(ISpreadMeasure spreadMeasure)
        {
            IMeasure volume = SpreadMeasures.GetValidSpreadMeasure(MeasureUnitTypeCode.VolumeUnit, spreadMeasure);

            return GetSpreadFactory().Create((IVolume)volume);
        }

        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);

        public abstract IDryBody Create(IDryBody other);

        public IPlaneShape CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public abstract IPlaneShapeFactory GetBaseFaceFactory();

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
        //public abstract TContext CreateBaseFace(params IExtent[] shapeExtents);
    }
}
