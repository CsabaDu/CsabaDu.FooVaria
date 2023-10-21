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
}
