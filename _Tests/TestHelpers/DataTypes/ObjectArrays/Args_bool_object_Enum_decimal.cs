namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_object_Enum_Decimal(string Case, bool IsTrue, object Obj, Enum MeasureUnit, decimal Quantity) : Args_bool_object_Enum(Case, IsTrue, Obj, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, IsTrue, Obj, MeasureUnit, Quantity];
}
