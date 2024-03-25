namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_Decimal_Object_TypeCode_args(Enum MeasureUnit, decimal DefaultQuantity, object Obj, TypeCode TypeCode) : Enum_Decimal_Object_args(MeasureUnit, DefaultQuantity, Obj)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, Obj, TypeCode];
}
