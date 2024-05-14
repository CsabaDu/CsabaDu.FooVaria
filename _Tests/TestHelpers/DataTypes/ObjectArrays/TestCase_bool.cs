namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool(string Case, bool IsTrue) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue];
    }

    public record TestCase_Enum_MeasureUnitCode_MeasureUnitCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, MeasureUnitCode Other) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, Other];
    }

    public record TestCase_Enum_MeasureUnitCode_MeasureUnitCode_RateComponentCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, MeasureUnitCode Other, RateComponentCode RateComponentCode) : TestCase_Enum_MeasureUnitCode_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode, Other)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, Other, RateComponentCode];
    }
}