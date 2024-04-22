namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public abstract record ObjectArray(string Case)
{
    public virtual object[] ToObjectArray() => [Case, Case];
}
