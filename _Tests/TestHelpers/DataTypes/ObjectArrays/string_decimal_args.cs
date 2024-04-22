namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record string_decimal_args(string Case, string ParamName, decimal DecimalQuantity) : string_args(Case, ParamName)
{
    public override object[] ToObjectArray() => [Case, ParamName, DecimalQuantity];
}
