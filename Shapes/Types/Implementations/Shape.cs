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

        public override Enum GetMeasureUnit()
        {
            throw new NotImplementedException();
        }

        public override ISpreadMeasure GetSpreadMeasure()
        {
            throw new NotImplementedException();
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            throw new NotImplementedException();
        }
    }
}
