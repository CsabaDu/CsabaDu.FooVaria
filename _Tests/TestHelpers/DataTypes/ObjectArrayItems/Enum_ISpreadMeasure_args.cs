namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Enum_ISpreadMeasure_args(Enum MeasureUnit, ISpreadMeasure SpreadMeasure) : Enum_arg(MeasureUnit)
    {
        public override object[] ToObjectArray() => [MeasureUnit, SpreadMeasure];
    }
}

