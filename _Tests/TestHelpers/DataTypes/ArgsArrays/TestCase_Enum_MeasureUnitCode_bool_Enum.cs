using CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_MeasureUnitCode_bool_Enum(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue, Enum Context) : TestCase_Enum_MeasureUnitCode_bool(TestCase, MeasureUnit, MeasureUnitCode, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, IsTrue, Context];
    }
    public record TestCase_Enum_MeasureUnitCode_IBaseRate(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, IBaseRate BaseRate) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, BaseRate];
    }
}
