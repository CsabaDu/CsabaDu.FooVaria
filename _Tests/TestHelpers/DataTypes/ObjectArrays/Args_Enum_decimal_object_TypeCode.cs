namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal_object_TypeCode(string Case, Enum MeasureUnit, decimal DefaultQuantity, object Obj, TypeCode TypeCode) : Args_Enum_decimal_object(Case, MeasureUnit, DefaultQuantity, Obj)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Obj, TypeCode];
}
