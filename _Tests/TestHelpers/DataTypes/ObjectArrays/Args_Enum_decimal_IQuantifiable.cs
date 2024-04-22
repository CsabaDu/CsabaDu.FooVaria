namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal_IQuantifiable(string Case, Enum MeasureUnit, decimal DefaultQuantity, IQuantifiable Quantifiable) : Args_Enum_decimal(Case, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Quantifiable];
}
