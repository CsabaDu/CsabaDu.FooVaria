namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record TypeCode_arg(TypeCode TypeCode) : ObjectArray
{
    public override object[] ToObjectArray() => [TypeCode];
}
