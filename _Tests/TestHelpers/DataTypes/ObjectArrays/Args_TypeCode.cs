namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_TypeCode(string Case, TypeCode TypeCode) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, TypeCode];
}
