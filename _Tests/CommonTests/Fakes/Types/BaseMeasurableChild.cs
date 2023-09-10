using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Common.Types;
using CsabaDu.FooVaria.Common.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.CommonTests.Fakes.Types
{
    internal class BaseMeasurableChild : BaseMeasurable
    {
        public BaseMeasurableChild(MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
        {
        }

        public BaseMeasurableChild(Enum measureUnit) : base(measureUnit)
        {
        }

        public BaseMeasurableChild(IBaseMeasurable baseMeasurable) : base(baseMeasurable)
        {
        }

        public override Enum GetMeasureUnit()
        {
            return GetDefaultMeasureUnit();
        }
    }
}
