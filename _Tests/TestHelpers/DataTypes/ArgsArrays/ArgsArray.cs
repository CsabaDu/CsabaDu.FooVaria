namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public abstract record ArgsArray(string TestCase)
{
    public virtual object[] ToObjectArray() => [TestCase];
}
