namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_MeasureUnitCode(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode) : TestCase_Enum(TestCase, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode];
}
