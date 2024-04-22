namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_Enum_LimitMode_IQuantifiable(string TestCase, Enum MeasureUnit, LimitMode? LimitMode, IQuantifiable Quantifiable) : TestCase_Enum_LimitMode(TestCase, MeasureUnit, LimitMode)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, LimitMode, Quantifiable];
}
