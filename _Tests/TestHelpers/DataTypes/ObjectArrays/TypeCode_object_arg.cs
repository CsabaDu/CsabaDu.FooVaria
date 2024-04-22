namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TypeCode_object_args(string Case, TypeCode TypeCode, object Object) : TypeCode_args(Case, TypeCode)
{
    public override object[] ToObjectArray() => [Case, TypeCode, Object];
}
