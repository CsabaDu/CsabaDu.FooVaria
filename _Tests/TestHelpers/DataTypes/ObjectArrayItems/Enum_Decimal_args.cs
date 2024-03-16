namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_Decimal_args(Enum MeasureUnit, decimal ExchangeRate) : Enum_arg(MeasureUnit)
{
    public override object[] ToObjectArray() => [MeasureUnit, ExchangeRate];
}
