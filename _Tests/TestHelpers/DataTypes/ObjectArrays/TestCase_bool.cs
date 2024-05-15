namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool(string Case, bool IsTrue) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue];
    }

    public record TestCase_Enum_MeasureUnitCode_RateComponentCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode];
    }

    public record TestCase_Enum_MeasureUnitCode_RateComponentCode_MeasureUnitCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode, MeasureUnitCode Other) : TestCase_Enum_MeasureUnitCode_RateComponentCode(TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode, Other];
    }

    public record TestCase_Enum_MeasureUnitCode_RateComponentCode_object(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode, object Obj) : TestCase_Enum_MeasureUnitCode_RateComponentCode(TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode, Obj];
    }
}