namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_MeasureUnitCode_RateComponentCode_object(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode, object Obj) : TestCase_Enum_MeasureUnitCode_RateComponentCode(TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode, Obj];
    }

    public record TestCase_Enum_MeasureUnitCode_RateComponentCode_object_bool(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode, object Obj, bool IsTrue) : TestCase_Enum_MeasureUnitCode_RateComponentCode_object(TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode, Obj)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode, Obj, IsTrue];
    }
}