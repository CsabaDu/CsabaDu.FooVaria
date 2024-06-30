namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_bool_Enum_Enum(string Case, bool IsTrue, Enum MeasureUnit, Enum Context) : TestCase_bool_Enum(Case, IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MeasureUnit, Context];
}
