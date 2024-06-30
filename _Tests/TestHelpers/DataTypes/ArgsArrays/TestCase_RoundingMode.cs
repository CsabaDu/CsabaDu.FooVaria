namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record RoundingMode_args(string TestCase, RoundingMode RoundingMode) : ArgsArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, RoundingMode];
}
