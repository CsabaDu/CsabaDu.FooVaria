namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;
public record Bool_Object_args(bool IsTrue, object Obj) : Bool_arg(IsTrue)
{
    public override object[] ToObjectArray() => [IsTrue, Obj];
}
