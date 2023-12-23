namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseShape : BaseSpread, IBaseShape
    {
        protected BaseShape(IBaseShape other) : base(other)
        {
        }

        protected BaseShape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
        }

        protected BaseShape(IBaseSpreadFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitTypeCode, shapeComponents)
        {
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes().Append(MeasureUnitTypeCode.ExtentUnit);
        }

        public override IBaseShapeFactory GetFactory()
        {
            return (IBaseShapeFactory)Factory;
        }

        public virtual bool Equals(IBaseShape? other)
        {
            return base.Equals(other)
                && GetShapeComponents().SequenceEqual(other.GetShapeComponents());
        }

        public abstract int CompareTo(IBaseShape? other);
        public abstract bool? FitsIn(IBaseShape? other, LimitMode? limitMode);
        public int GetShapeComponentCount()
        {
            return GetShapeComponents().Count();
        }
        public abstract void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);

        public abstract IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);
        public abstract IEnumerable<IShapeComponent> GetShapeComponents();
    }
}
