namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_MeasureUnitCode_RateComponentCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, RateComponentCode RateComponentCode) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, RateComponentCode];
    }
}