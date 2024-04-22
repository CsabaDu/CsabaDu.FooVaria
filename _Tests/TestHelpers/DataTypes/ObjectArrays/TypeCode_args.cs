namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TypeCode_args(string Case, TypeCode TypeCode) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, TypeCode];
}
