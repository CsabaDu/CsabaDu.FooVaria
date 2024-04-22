namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Enum_ISpreadMeasure_IQuantifiable_args(string Case, Enum MeasureUnit, ISpreadMeasure SpreadMeasure, IQuantifiable Quantifiable) : Enum_ISpreadMeasure_args(Case, MeasureUnit, SpreadMeasure)
    {
        public override object[] ToObjectArray() => [Case, MeasureUnit, SpreadMeasure, Quantifiable];
    }
}

