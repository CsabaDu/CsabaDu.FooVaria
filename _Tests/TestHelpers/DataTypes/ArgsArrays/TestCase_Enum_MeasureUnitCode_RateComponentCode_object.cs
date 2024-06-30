namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_MeasureUnitCode_RateComponentCode_object(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode, object Obj) : TestCase_Enum_MeasureUnitCode_RateComponentCode(TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode, Obj];
    }
}