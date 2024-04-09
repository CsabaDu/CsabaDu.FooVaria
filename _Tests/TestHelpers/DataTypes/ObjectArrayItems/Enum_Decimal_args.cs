namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_decimal_args(Enum MeasureUnit, decimal DefaultQuantity) : Enum_arg(MeasureUnit)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity];
}
