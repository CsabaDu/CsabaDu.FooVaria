using CsabaDu.FooVaria.Common.Types.Implementations;

namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal abstract class Shape : BaseShape, IShape
    {
        private protected Shape(IShape other) : base(other)
        {
        }

        private protected Shape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
        }

        public abstract ISpread Spread { get; }

        public override ISpreadMeasure GetSpreadMeasure()
        {
            return Spread.GetSpreadMeasure();
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
        }
    }
}
