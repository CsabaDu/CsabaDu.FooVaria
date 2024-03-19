namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_LimitMode_IQuantifiable_arg(Enum MeasureUnit, LimitMode? LimitMode, IQuantifiable Quantifiable) : Enum_LimitMode_arg(MeasureUnit, LimitMode)
{
    public override object[] ToObjectArray() => [MeasureUnit, LimitMode, Quantifiable];
}
