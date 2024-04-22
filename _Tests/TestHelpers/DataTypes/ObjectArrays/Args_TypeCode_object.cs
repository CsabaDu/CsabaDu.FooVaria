namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_TypeCode_object(string Case, TypeCode TypeCode, object Object) : Args_TypeCode(Case, TypeCode)
{
    public override object[] ToObjectArray() => [Case, TypeCode, Object];
}
