namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_MeasureUnitCode_bool_MeasureUnitCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue, MeasureUnitCode Other) : TestCase_Enum_MeasureUnitCode_bool(TestCase, MeasureUnit, MeasureUnitCode, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, IsTrue, Other];
    }
}
