namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_object_Enum_decimal_args(bool IsTrue, object Obj, Enum MeasureUnit, decimal Quantity) : bool_object_Enum_args(IsTrue, Obj, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit, Quantity];
}
