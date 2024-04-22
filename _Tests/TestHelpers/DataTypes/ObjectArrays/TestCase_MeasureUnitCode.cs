namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_MeasureUnitCode(string TestCase, MeasureUnitCode MeasureUnitCode) : ObjectArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnitCode];
}
