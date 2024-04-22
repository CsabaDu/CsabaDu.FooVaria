namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_MeasureUnitCode_IMeasurable(string Case, MeasureUnitCode MeasureUnitCode, IMeasurable Measurable) : MeasureUnitCode_args(Case, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [Case, MeasureUnitCode, Measurable];
}
