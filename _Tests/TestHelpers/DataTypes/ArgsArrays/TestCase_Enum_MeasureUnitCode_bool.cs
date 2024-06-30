namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_MeasureUnitCode_bool(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, IsTrue];
}
