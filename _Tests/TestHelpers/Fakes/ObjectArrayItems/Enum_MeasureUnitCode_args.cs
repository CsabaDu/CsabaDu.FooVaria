namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.ObjectArrayItems;

public record Enum_MeasureUnitCode_args(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode) : Enum_arg(MeasureUnit)
{
    public override object[] ToObjectArray() => [MeasureUnit, MeasureUnitCode];
}
