namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record Enum_MeasureUnitCode_args(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode) : Enum_arg(MeasureUnit)
{
    public override object[] ToObjectArray() => [MeasureUnit, MeasureUnitCode];
}
