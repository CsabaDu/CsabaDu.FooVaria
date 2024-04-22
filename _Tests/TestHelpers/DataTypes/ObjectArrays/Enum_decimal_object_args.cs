namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_decimal_object_args(string Case, Enum MeasureUnit, decimal DefaultQuantity, object Obj) : Enum_decimal_args(Case, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Obj];
}
