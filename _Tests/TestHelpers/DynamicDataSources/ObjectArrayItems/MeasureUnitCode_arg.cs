namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record MeasureUnitCode_arg(MeasureUnitCode MeasureUnitCode) : ObjectArray
{
    public override object[] ToObjectArray() => [MeasureUnitCode];
}
