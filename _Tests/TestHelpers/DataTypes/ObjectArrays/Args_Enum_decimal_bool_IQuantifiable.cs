namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal_bool_IQuantifiable(string Case, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, IQuantifiable Quantifiable) : Args_Enum_decimal_bool(Case, MeasureUnit, DefaultQuantity, IsTrue)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, IsTrue, Quantifiable];
}
