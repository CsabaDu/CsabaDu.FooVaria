namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.ObjectArrayItems;

public record Enum_MeasureUnitCode_bool_args(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue) : Enum_MeasureUnitCode_args(MeasureUnit, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [MeasureUnit, MeasureUnitCode, IsTrue];
}
