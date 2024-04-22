namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_decimal_bool_IQuantifiable_args(string Case, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, IQuantifiable Quantifiable) : Enum_decimal_bool_args(Case, MeasureUnit, DefaultQuantity, IsTrue)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, IsTrue, Quantifiable];
}
