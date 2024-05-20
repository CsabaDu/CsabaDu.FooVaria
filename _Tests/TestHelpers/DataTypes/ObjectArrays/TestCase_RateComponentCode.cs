namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_RateComponentCode(string Case, RateComponentCode RateComponentCode) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [TestCase, RateComponentCode];
}