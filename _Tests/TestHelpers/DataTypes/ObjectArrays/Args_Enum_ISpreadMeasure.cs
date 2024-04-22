namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_ISpreadMeasure(string Case, Enum MeasureUnit, ISpreadMeasure SpreadMeasure) : Args_Enum(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, SpreadMeasure];
}
