namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_LimitMode_IQuantifiable_args(string Case, Enum MeasureUnit, LimitMode? LimitMode, IQuantifiable Quantifiable) : Enum_LimitMode_args(Case, MeasureUnit, LimitMode)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, LimitMode, Quantifiable];
}
