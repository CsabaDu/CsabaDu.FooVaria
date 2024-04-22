namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_MeasureUnitCode(string Case, bool IsTrue, MeasureUnitCode MeasureUnitCode) : bool_args(Case, IsTrue)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnitCode];
}
