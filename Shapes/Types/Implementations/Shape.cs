using CsabaDu.FooVaria.Common.Types.Implementations;

namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal class Shape : BaseShape, IShape
    {
        public Shape(IBaseShape other) : base(other)
        {
        }

        public Shape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
        }

        public ISpread Spread { get; }

        public override IBaseSpread? ExchangeTo(Enum measureUnit)
        {
            return Spread.ExchangeTo(measureUnit);
        }

        public override ISpreadMeasure GetSpreadMeasure()
        {
            return Spread.GetSpreadMeasure();
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return Spread.IsValidMeasureUnitTypeCode(measureUnitTypeCode);
        }

        public override void ValidateQuantity(ValueType? quantity)
        {
            throw new NotImplementedException();
        }
    }
}
