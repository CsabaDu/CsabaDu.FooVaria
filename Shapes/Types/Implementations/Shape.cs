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

        public override decimal DefaultQuantity => Spread.DefaultQuantity;

        public override int CompareTo(IBaseSpread? other)
        {
            throw new NotImplementedException();
        }

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

        public override decimal ProportionalTo(IBaseSpread other)
        {
            throw new NotImplementedException();
        }
    }
}
