namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.ObjectArrayItems;

public record Bool_Object_Enum_args(bool IsTrue, object Obj, Enum MeasureUnit) : Bool_Object_args(IsTrue, Obj)
{
    public override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit];
}
