namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_decimal_object_args(Enum MeasureUnit, decimal DefaultQuantity, object Obj) : Enum_decimal_args(MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, Obj];
}
