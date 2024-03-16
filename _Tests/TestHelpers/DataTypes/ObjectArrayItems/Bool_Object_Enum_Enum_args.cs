namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Bool_Object_Enum_Enum_args(bool IsTrue, object Obj, Enum MeasureUnit, Enum OtherMeasureUnit) : Bool_Object_Enum_args(IsTrue, Obj, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit, OtherMeasureUnit];
}
