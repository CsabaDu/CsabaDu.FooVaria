namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_Decimal_Object_args(Enum MeasureUnit, decimal DefaultQuantity, object Obj) : Enum_Decimal_args(MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, Obj];
}
