namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_Enum_LimitMode(string TestCase, Enum MeasureUnit, LimitMode? LimitMode) : TestCase_Enum(TestCase, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, LimitMode];
}
