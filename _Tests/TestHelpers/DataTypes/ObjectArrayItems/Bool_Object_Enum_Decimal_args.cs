namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Bool_Object_Enum_Decimal_args(bool IsTrue, object Obj, Enum MeasureUnit, decimal Quantity) : Bool_Object_Enum_args(IsTrue, Obj, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit, Quantity];
}
