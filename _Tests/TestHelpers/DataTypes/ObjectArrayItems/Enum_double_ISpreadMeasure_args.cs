namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_double_ISpreadMeasure_args(Enum MeasureUnit, double Quantity, ISpreadMeasure SpreadMeasure) : Enum_double_args(MeasureUnit, Quantity)
{
    public override object[] ToObjectArray() => [MeasureUnit, Quantity, SpreadMeasure];
}

