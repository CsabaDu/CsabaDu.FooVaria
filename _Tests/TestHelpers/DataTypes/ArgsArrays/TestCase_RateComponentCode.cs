namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_RateComponentCode(string Case, RateComponentCode RateComponentCode) : ArgsArray(Case)
{
    public override object[] ToObjectArray() => [TestCase, RateComponentCode];
}