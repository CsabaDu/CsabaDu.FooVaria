namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_Enum(string TestCase, Enum MeasureUnit) : ObjectArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit];
}
