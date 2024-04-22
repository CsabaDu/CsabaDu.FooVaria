namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_double_ISpreadMeasure_args(string Case, Enum MeasureUnit, double Quantity, ISpreadMeasure SpreadMeasure) : Enum_double_args(Case, MeasureUnit, Quantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Quantity, SpreadMeasure];
}

