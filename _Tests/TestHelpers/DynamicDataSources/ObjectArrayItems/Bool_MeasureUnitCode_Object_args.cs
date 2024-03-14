namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record Bool_MeasureUnitCode_Object_args(bool IsTrue, MeasureUnitCode MeasureUnitCode, object Obj) : Bool_MeasureUnitCode_args(IsTrue, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [IsTrue];
}
