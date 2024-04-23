namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public abstract record ObjectArray(string TestCase)
{
    public virtual object[] ToObjectArray() => [TestCase];
}
