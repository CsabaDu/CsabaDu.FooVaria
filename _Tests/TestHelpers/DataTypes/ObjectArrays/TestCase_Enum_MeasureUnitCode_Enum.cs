namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_Enum_MeasureUnitCode_Enum(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, Enum Context) : TestCase_Enum_MeasureUnitCode(TestCase, MeasureUnit, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, Context];
}
