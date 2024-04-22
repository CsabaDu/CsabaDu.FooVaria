namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_decimal_bool_args(string Case, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue) : Enum_decimal_args(Case, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, IsTrue];
}
