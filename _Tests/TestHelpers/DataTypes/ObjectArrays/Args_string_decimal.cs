namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_string_decimal(string Case, string ParamName, decimal DecimalQuantity) : Args_string(Case, ParamName)
{
    public override object[] ToObjectArray() => [Case, ParamName, DecimalQuantity];
}
