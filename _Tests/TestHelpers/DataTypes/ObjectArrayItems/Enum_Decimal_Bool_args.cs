namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_decimal_bool_args(Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue) : Enum_decimal_args(MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, IsTrue];
}
