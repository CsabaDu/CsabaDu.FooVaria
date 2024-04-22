namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_ISpreadMeasure_IQuantifiable(string Case, Enum MeasureUnit, ISpreadMeasure SpreadMeasure, IQuantifiable Quantifiable) : Args_Enum_ISpreadMeasure(Case, MeasureUnit, SpreadMeasure)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, SpreadMeasure, Quantifiable];
}

