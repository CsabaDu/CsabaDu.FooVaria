namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Enum_ISpreadMeasure_IQuantifiable_args(Enum MeasureUnit, ISpreadMeasure SpreadMeasure, IQuantifiable Quantifiable) : Enum_ISpreadMeasure_args(MeasureUnit, SpreadMeasure)
    {
        public override object[] ToObjectArray() => [MeasureUnit, SpreadMeasure, Quantifiable];
    }
}

