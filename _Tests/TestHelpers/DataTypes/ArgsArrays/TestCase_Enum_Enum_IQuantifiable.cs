namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_Enum_IQuantifiable(string TestCase, Enum MeasureUnit, Enum Context, IQuantifiable Quantifiable) : TestCase_Enum_Enum(TestCase, MeasureUnit, Context)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, Context, Quantifiable];
}
