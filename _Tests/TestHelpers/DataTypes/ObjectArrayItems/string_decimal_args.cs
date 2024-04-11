namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record string_decimal_args(string ParamName, decimal DecimalQuantity) : string_arg(ParamName)
{
    public override object[] ToObjectArray() => [ParamName, DecimalQuantity];
}
