using CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_MeasureUnitCode_bool_IBaseRate(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue, IBaseRate BaseRate) : TestCase_Enum_MeasureUnitCode_bool(TestCase, MeasureUnit, MeasureUnitCode, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, IsTrue, BaseRate];
    }
}
