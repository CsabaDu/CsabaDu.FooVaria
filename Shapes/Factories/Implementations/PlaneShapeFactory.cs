namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public abstract class PlaneShapeFactory : ShapeFactory, IPlaneShapeFactory
    {
        private protected PlaneShapeFactory(IBulkSurfaceFactory spreadFactory, ITangentShapeFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }

        public abstract IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular);

        public override IBulkSurface Create(ISpreadMeasure spreadMeasure)
        {
            IMeasure area = SpreadMeasures.GetValidSpreadMeasure(MeasureUnitTypeCode.AreaUnit, spreadMeasure);

            return GetSpreadFactory().Create((IArea)area);
        }

        public override sealed IBulkSurfaceFactory GetSpreadFactory()
        {
            return (IBulkSurfaceFactory)SpreadFactory;
        }

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

        public abstract IPlaneShape Create(IPlaneShape other);
    }
}
