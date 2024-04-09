namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_decimal_bool_IQuantifiable_args(Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, IQuantifiable Quantifiable) : Enum_decimal_bool_args(MeasureUnit, DefaultQuantity, IsTrue)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, IsTrue, Quantifiable];
}
