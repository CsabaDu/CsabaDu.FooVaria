namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_bool_Enum(string Case, bool IsTrue, Enum MeasureUnit) : TestCase_bool(Case, IsTrue)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MeasureUnit];
}
