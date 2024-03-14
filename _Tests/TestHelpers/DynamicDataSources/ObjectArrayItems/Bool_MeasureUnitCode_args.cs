namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record Bool_MeasureUnitCode_args(bool IsTrue, MeasureUnitCode MeasureUnitCode) : Bool_arg(IsTrue)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnitCode];
}
