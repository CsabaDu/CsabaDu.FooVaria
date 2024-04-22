namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record RoundingMode_args(string TestCase, RoundingMode RoundingMode) : ObjectArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, RoundingMode];
}
