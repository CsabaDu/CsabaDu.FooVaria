namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_ILimiter(string TestCase, Enum MeasureUnit, ILimiter Limiter) : TestCase_Enum(TestCase, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, Limiter];
}
