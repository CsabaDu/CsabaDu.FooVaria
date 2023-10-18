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

    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        foreach (MeasureUnitTypeCode item in base.GetMeasureUnitTypeCodes())
        {
            yield return item;
        }

        yield return MeasureUnitTypeCode.ExtentUnit;
    }
}

    public abstract class BaseRate : BaseMeasurable, IBaseRate
    {
        protected BaseRate(IBaseRate other) : base(other)
        {
        }

        protected BaseRate(IFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        protected BaseRate(IFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected BaseRate(IFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
        {
        }

        public abstract decimal GetDefaultQuantity();
        public abstract MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
    }
}
