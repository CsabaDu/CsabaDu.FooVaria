namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_MeasureUnitCode(string Case, bool IsTrue, MeasureUnitCode MeasureUnitCode) : Args_bool(Case, IsTrue)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnitCode];
}
