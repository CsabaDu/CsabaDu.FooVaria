namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_decimal_args(string Case, Enum MeasureUnit, decimal DefaultQuantity) : Enum_args(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity];
}
