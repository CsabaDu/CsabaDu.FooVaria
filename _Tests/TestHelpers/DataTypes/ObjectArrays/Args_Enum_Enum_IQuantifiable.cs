namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_Enum_IQuantifiable(string Case, Enum MeasureUnit, Enum Context, IQuantifiable Quantifiable) : Args_Enum_Enum(Case, MeasureUnit, Context)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Context, Quantifiable];
}
