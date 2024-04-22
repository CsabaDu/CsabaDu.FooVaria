namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Enum_ISpreadMeasure_args(string Case, Enum MeasureUnit, ISpreadMeasure SpreadMeasure) : Enum_args(Case, MeasureUnit)
    {
        public override object[] ToObjectArray() => [Case, MeasureUnit, SpreadMeasure];
    }
}

