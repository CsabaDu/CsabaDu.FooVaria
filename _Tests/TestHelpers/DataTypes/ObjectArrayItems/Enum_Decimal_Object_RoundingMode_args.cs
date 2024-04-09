namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_decimal_object_RoundingMode_args(Enum MeasureUnit, decimal DefaultQuantity, object Obj, RoundingMode RoundingMode) : Enum_decimal_object_args(MeasureUnit, DefaultQuantity, Obj)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, Obj, RoundingMode];
}
