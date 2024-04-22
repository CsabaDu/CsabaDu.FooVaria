namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record MeasureUnitCode_args(string Case, MeasureUnitCode MeasureUnitCode) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, MeasureUnitCode];
}
