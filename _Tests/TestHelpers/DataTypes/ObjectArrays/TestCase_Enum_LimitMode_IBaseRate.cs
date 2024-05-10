using CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_LimitMode_IBaseRate(string TestCase, Enum MeasureUnit, LimitMode? LimitMode, IBaseRate BaseRate) : TestCase_Enum_LimitMode(TestCase, MeasureUnit, LimitMode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, LimitMode, BaseRate];
    }

    public record TestCase_Enum_LimitMode_IBaseRate_MeasureUnitCode(string TestCase, Enum MeasureUnit, LimitMode? LimitMode, IBaseRate BaseRate, MeasureUnitCode MeasureUnitCode) : TestCase_Enum_LimitMode_IBaseRate(TestCase, MeasureUnit, LimitMode, BaseRate)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, LimitMode, BaseRate, MeasureUnitCode];
    }
}
