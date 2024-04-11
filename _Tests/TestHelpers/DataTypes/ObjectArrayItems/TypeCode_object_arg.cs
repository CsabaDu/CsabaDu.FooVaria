namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record TypeCode_object_arg(TypeCode TypeCode, object Object) : TypeCode_arg(TypeCode)
{
    public override object[] ToObjectArray() => [TypeCode, Object];
}
