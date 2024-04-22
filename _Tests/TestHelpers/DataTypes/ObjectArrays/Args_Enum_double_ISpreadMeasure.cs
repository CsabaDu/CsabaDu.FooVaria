namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_double_ISpreadMeasure(string Case, Enum MeasureUnit, double Quantity, ISpreadMeasure SpreadMeasure) : Args_Enum_double(Case, MeasureUnit, Quantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Quantity, SpreadMeasure];
}

