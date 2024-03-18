namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_Enum_IQuantifiable_args(Enum MeasureUnit, Enum Context, IQuantifiable Quantifiable) : Enum_Enum_args(MeasureUnit, Context)
{
    public override object[] ToObjectArray() => [MeasureUnit, Context, Quantifiable];
}
