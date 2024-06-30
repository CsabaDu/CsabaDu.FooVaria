namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_MeasureUnitCode_MeasureUnitCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, MeasureUnitCode Other) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, Other];
    }
}