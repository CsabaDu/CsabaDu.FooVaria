namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record bool_object_Enum_decimal_args(string Case, bool IsTrue, object Obj, Enum MeasureUnit, decimal Quantity) : bool_object_Enum_args(Case, IsTrue, Obj, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, IsTrue, Obj, MeasureUnit, Quantity];
}
