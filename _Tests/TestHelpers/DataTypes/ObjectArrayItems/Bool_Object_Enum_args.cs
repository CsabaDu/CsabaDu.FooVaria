namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Bool_Object_Enum_args(bool IsTrue, object Obj, Enum MeasureUnit) : Bool_Object_args(IsTrue, Obj)
{
    public override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit];
}
