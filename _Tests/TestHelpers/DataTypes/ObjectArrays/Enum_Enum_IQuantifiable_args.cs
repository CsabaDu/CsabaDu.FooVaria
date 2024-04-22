namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_Enum_IQuantifiable_args(string Case, Enum MeasureUnit, Enum Context, IQuantifiable Quantifiable) : Enum_Enum_args(Case, MeasureUnit, Context)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Context, Quantifiable];
}
