namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_bool_MeasureUnitCode(string TestCase, bool IsTrue, MeasureUnitCode MeasureUnitCode) : TestCase_bool(TestCase, IsTrue)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MeasureUnitCode];
}
